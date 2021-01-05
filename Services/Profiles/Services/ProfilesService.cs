using System.Collections.Generic;
using MongoDB.Driver;
using Profiles.Interfaces;
using Profiles.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Profiles.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly IMongoRepository<ProfileEntity> _mongoRepository;

        public ProfilesService(IMongoRepository<ProfileEntity> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public List<ProfileEntity?> GetAll(string profileId)
        {
            return _mongoRepository.GetAll(profileId)!;
        }

        public ProfileEntity? Get(string id)
        {
            return _mongoRepository.Get(id);
        }

        public ProfileEntity? Create(ProfileEntity entity)
        {
            return _mongoRepository.Create(entity);
        }

        public ProfileEntity? Update(string id, ProfileEntity entity)
        {
            return _mongoRepository.Update(id, entity);
        }

        public IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChanges(string profileId)
        {
            return _mongoRepository.SubscribeToChangesStream(profileId)!;
        }
    }
}