using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Profiles.Interfaces;
using Profiles.Mappers;
using Profiles.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;
using Sdk.Persistence.Extensions;

// https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-5.0
namespace Profiles.Hubs
{
    [Authorize("read:profiles")]
    public class ProfilesHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfilesService _profilesService;

        public ProfilesHub(
            IProfilesService profilesService,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _profilesService = profilesService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> RequestAll(int count)
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var profiles = _profilesService.GetManyByParameter(
                e => e.ProfileId == profileId, 
                count
            );
            var profilesDto = profiles
                .Select(n => n?.ToProfileDto())
                .ToList();
            await Clients.All.SendAsync("ResponseAll", profilesDto);

            var pipeline = profileId.ToMongoPipelineMatchMany<ProfileEntity>();
            var enumerator = _profilesService.SubscribeToChangesMany(pipeline);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "ResponseAll",
                        new List<ProfileDto>(){enumerator.Current.FullDocument.ToProfileDto()}
                    );
            return true;
        }

        public async Task<bool> RequestOne(string accountId, int count)
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var profile = _profilesService.GetSingleByParameter(
                e => e.ProfileId == profileId && e.AccountId == accountId
            );
            // TODO: filter transactions
            
            await Clients.All.SendAsync("ResponseOne", profile?.ToProfileDto());

            var pipeline = profileId.ToMongoPipelineMatchSingle<ProfileEntity>(accountId);
            var enumerator = _profilesService.SubscribeToChangesMany(pipeline);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "ResponseOne",
                        enumerator.Current.FullDocument.ToProfileDto()
                    );
            return true;
        }
    }
}