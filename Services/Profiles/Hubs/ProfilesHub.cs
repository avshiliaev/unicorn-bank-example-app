using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Profiles.Interfaces;
using Profiles.Mappers;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;

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
            var profiles = _profilesService.GetAll(profileId);
            var profilesDto = profiles
                .Select(n => n.ToProfilesModel<ProfileDto>())
                .ToList();
            await Clients.All.SendAsync("Response", profilesDto);

            var enumerator = _profilesService.SubscribeToChanges(profileId);
            while (enumerator.MoveNext())
                if (enumerator.Current != null)
                    await Clients.All.SendAsync(
                        "Response",
                        enumerator.Current.FullDocument.ToProfilesModel<ProfileDto>()
                    );
            return true;
        }
    }
}