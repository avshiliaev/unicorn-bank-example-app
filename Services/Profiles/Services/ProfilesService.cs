using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
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

        public List<ProfileEntity?> GetManyByParameter(Expression<Func<ProfileEntity, bool>> predicate, int count)
        {
            return _mongoRepository.GetManyByParameter(predicate, count)!;
        }

        public ProfileEntity? GetSingleByParameter(Expression<Func<ProfileEntity, bool>> predicate)
        {
            return _mongoRepository.GetSingleByParameter(predicate)!;
        }

        public ProfileEntity? Get(string id)
        {
            return _mongoRepository.Get(id);
        }

        public ProfileEntity? Create(ProfileEntity entity)
        {
            return _mongoRepository.Create(entity);
        }

        public ProfileEntity? UpdatePassively(ProfileEntity profileEntity)
        {
            using var session = _mongoRepository.Client.StartSession();
            session.StartTransaction();
            try
            {
                var filter = Builders<ProfileEntity>.Filter.And(
                    Builders<ProfileEntity>.Filter.Eq(p => p.EntityId, profileEntity.EntityId),
                    Builders<ProfileEntity>.Filter.Eq(p => p.Version, profileEntity.Version - 1)
                );
                var updateDefinition = Builders<ProfileEntity>.Update
                    .Set("Balance", profileEntity.Balance)
                    .Set("Version", profileEntity.Version)
                    .Set("Approved", profileEntity.Approved)
                    .Set("Pending", profileEntity.Pending)
                    .Set("Blocked", profileEntity.Blocked);

                var result = _mongoRepository.Update(filter, updateDefinition);
                session.CommitTransaction();
                if (result != null && !string.IsNullOrEmpty(result.Id))
                    return result;
                return null;
            }
            catch
            {
                session.AbortTransaction();
                return null;
            }
        }

        public ProfileEntity? AddToArray(string accountId, TransactionSubEntity transactionSubEntity)
        {
            using var session = _mongoRepository.Client.StartSession();
            session.StartTransaction();
            try
            {
                var filter = Builders<ProfileEntity>.Filter.And(
                    Builders<ProfileEntity>.Filter.Eq(p => p.EntityId, accountId)
                );
                var updateDefinition = Builders<ProfileEntity>.Update.Push(
                    "Transactions",
                    transactionSubEntity
                );
                var result = _mongoRepository.Update(filter, updateDefinition);
                session.CommitTransaction();
                if (result != null && !string.IsNullOrEmpty(result.Id))
                    return result;
                return null;
            }
            catch
            {
                session.AbortTransaction();
                return null;
            }
        }

        public ProfileEntity? UpdateInArray(string accountId, TransactionSubEntity transactionSubEntity)
        {
            using var session = _mongoRepository.Client.StartSession();
            session.StartTransaction();
            try
            {
                var profile = _mongoRepository.GetSingleByParameter(p => p.EntityId == accountId);
                if (profile.Transactions.All(t => t.Id != transactionSubEntity.Id))
                    return null;

                var filter = new BsonDocument(
                    "AccountId", accountId
                );
                var pullTransaction = Builders<ProfileEntity>.Update.PullFilter(
                    "Transactions",
                    Builders<TransactionSubEntity>.Filter.Eq("_id", transactionSubEntity.Id)
                );
                var _ = _mongoRepository.Update(filter, pullTransaction);
                var pushTransaction = Builders<ProfileEntity>.Update.Push(
                    "Transactions",
                    transactionSubEntity
                );
                var pushed = _mongoRepository.Update(filter, pushTransaction);

                session.CommitTransaction();
                if (pushed != null && !string.IsNullOrEmpty(pushed.Id))
                    return pushed;
                return null;
            }
            catch
            {
                session.AbortTransaction();
                return null;
            }
        }

        public IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChangesMany(
            PipelineDefinition<ChangeStreamDocument<ProfileEntity>, ChangeStreamDocument<ProfileEntity>> pipeline
        )
        {
            return _mongoRepository.SubscribeToChangesStreamMany(pipeline)!;
        }
    }
}