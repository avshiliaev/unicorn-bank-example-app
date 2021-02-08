using MongoDB.Driver;
using Sdk.Persistence.Interfaces;

namespace Sdk.Persistence.Tools
{
    public static class MongoDbPipelineBuilder
    {
        public static PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>>
            MongoPipelineMatchMany<TEntity>(string profileId) where TEntity : class, IMongoEntity
        {
            return new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match(change =>
                    change.FullDocument.ProfileId == profileId &&
                    change.OperationType == ChangeStreamOperationType.Update
                );
        }

        public static PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>>
            MongoPipelineMatchSingle<TEntity>(string profileId, string accountId) where TEntity : class, IMongoEntity
        {
            return new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match(change =>
                    change.FullDocument.ProfileId == profileId &&
                    change.FullDocument.AccountId == accountId &&
                    change.OperationType == ChangeStreamOperationType.Update
                );
        }
    }
}