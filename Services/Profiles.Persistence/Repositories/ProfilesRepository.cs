using Profiles.Persistence.Entities;
using Sdk.Persistence.Abstractions;
using Sdk.Persistence.Interfaces;

namespace Profiles.Persistence.Repositories
{
    public class ProfilesRepository : AbstractMongoRepository<ProfileEntity>
    {
        public ProfilesRepository(IMongoSettingsModel settings) : base(settings)
        {
        }
    }
}