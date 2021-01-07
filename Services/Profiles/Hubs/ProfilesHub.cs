using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Profiles.Interfaces;
using Profiles.Mappers;
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

        public async Task<bool> RequestAll()
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var profiles = _profilesService.GetManyByParameter(
                e => e.ProfileId == profileId
            );
            var profilesDto = profiles
                .Select(n => n?.ToProfileDto())
                .ToList();
            await Clients.All.SendAsync("Response", profilesDto);

            var pipeline = profileId.ToMongoPipelineMatchMany();
            var enumerator = _profilesService.SubscribeToChangesMany(pipeline);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "Response",
                        enumerator.Current.FullDocument.ToProfileDto()
                    );
            return true;
        }

        public async Task<bool> RequestOne(string accountId)
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            var profiles = _profilesService.GetManyByParameter(
                e => e.ProfileId == profileId && e.AccountId == accountId
            );
            var profilesDto = profiles
                .Select(n => n?.ToProfileDto())
                .ToList();
            await Clients.All.SendAsync("Response", profilesDto);

            var pipeline = profileId.ToMongoPipelineMatchSingle(accountId);
            var enumerator = _profilesService.SubscribeToChangesMany(pipeline);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "Response",
                        enumerator.Current.FullDocument.ToProfileDto()
                    );
            return true;
        }
    }
}