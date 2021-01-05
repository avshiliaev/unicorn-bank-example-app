using System;
using System.Globalization;
using Profiles.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Profiles.Mappers
{
    public static class ProfilesModelMapper
    {
        public static ProfileEntity ToProfilesEntity(this IProfileModel profileModel)
        {
            return new ProfileEntity
            {
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = profileModel.Version
            };
        }
    }
}