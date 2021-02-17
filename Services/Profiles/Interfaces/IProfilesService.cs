using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Profiles.Persistence.Entities;

namespace Profiles.Interfaces
{
    public interface IProfilesService
    {
        List<ProfileEntity?> GetAll(string profileId);
        List<ProfileEntity?> GetManyByParameter(Expression<Func<ProfileEntity, bool>> predicate, int count);
        ProfileEntity? GetSingleByParameter(Expression<Func<ProfileEntity, bool>> predicate);
        ProfileEntity? Get(string id);
        ProfileEntity? Create(ProfileEntity entity);
        ProfileEntity? UpdatePassively(ProfileEntity profileEntity);
        ProfileEntity? AddToArray(string accountId, TransactionSubEntity transactionSubEntity);
        ProfileEntity? UpdateInArray(string accountId, TransactionSubEntity transactionSubEntity);

        IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChangesMany(
            PipelineDefinition<ChangeStreamDocument<ProfileEntity>, ChangeStreamDocument<ProfileEntity>> pipeline
        );
    }
}