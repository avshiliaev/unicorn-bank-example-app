using Profiles.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Profiles.Mappers
{
    public static class ProfilesEntityMapper
    {
        public static TModel ToProfilesModel<TModel>(this ProfileEntity profileEntity)
            where TModel : class, IProfileModel, new()
        {
            return new TModel
            {
                Id = profileEntity.Id.ToGuid(),
                Version = profileEntity.Version
            };
        }
    }
}