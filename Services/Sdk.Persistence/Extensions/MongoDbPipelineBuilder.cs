namespace Sdk.Persistence.Extensions
{
    public static class MongoDbPipelineBuilder
    {
        public static string ToMongoPipelineMatchMany(this string profileId)
        {
            var pipeline = $"{{ ProfileId: {profileId} }}";
            return pipeline;
        }
        
        public static string ToMongoPipelineMatchSingle(this string profileId, string accountId)
        {
            var pipeline = $"$and: [{{ ProfileId: {profileId} }}, {{ AccountId: {accountId} }}]";
            return pipeline;
        }
    }
}