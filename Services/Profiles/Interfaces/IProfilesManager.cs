using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Profiles.Interfaces
{
    public interface IProfilesManager
    {
        ProfileDto? AddNewProfile(IProfileModel notificationModel);
    }
}