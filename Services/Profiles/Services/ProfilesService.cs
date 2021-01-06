using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Profiles.Interfaces;
using Profiles.Persistence.Entities;
using Sdk.Persistence.Extensions;
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

        public List<ProfileEntity?> GetManyByParameter(Expression<Func<ProfileEntity, bool>> predicate)
        {
            return _mongoRepository.GetManyByParameter(predicate)!;
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

        public IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChangesMany(string pipeline)
        {
            return _mongoRepository.SubscribeToChangesStreamMany(pipeline)!;
        }
    }
}