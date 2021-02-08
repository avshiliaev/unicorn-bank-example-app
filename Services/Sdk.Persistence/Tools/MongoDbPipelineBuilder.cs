using MongoDB.Driver;
using Sdk.Interfaces;

namespace Sdk.Persistence.Tools
{
    public static class MongoDbPipelineBuilder
    {
        public static PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>>
            MongoPipelineMatchMany<TEntity>(string profileId) where TEntity : class, IRecord
        {
            return new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match(change =>
                    change.FullDocument.ProfileId == profileId &&
                    change.OperationType == ChangeStreamOperationType.Update
                );
        }

        public static PipelineDefinition<ChangeStreamDocument<TEntity>, ChangeStreamDocument<TEntity>>
            MongoPipelineMatchSingle<TEntity>(string profileId, string accountId) where TEntity : class, IRecord
        {
            return new EmptyPipelineDefinition<ChangeStreamDocument<TEntity>>()
                .Match(change =>
                    change.FullDocument.ProfileId == profileId &&
                    change.FullDocument.EntityId == accountId &&
                    change.OperationType == ChangeStreamOperationType.Update
                );
        }
    }
}