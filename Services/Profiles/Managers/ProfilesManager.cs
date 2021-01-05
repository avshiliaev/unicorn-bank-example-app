using Microsoft.Extensions.Logging;
using Profiles.Interfaces;
using Profiles.Mappers;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Profiles.Managers
{
    public class ProfilesManager : IProfilesManager
    {
        private readonly IProfilesService _profilesService;
        private ILogger<ProfilesManager> _logger;

        public ProfilesManager(
            ILogger<ProfilesManager> logger,
            IProfilesService profilesService
        )
        {
            _logger = logger;
            _profilesService = profilesService;
        }

        public ProfileDto? AddNewProfile(IProfileModel profileModel)
        {
            if (string.IsNullOrEmpty(profileModel.Id))
                return null;
            var profile = _profilesService.Create(profileModel.ToProfilesEntity());
            return profile.ToProfilesModel<ProfileDto>();
        }
    }
}