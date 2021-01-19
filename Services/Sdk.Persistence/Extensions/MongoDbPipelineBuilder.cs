using MongoDB.Bson;

namespace Sdk.Persistence.Extensions
{
    public static class MongoDbPipelineBuilder
    {
        public static BsonDocument[] ToMongoPipelineMatchMany(this string profileId)
        {
            var match = new BsonDocument 
            { 
                { 
                    "$match", 
                    new BsonDocument 
                    { 
                        {"ProfileId", $"'{profileId}'"} 
                    } 
                }
            };

            return new []{match};
        }

        public static BsonDocument[] ToMongoPipelineMatchSingle(this string profileId, string accountId)
        {
            // var pipeline = $"$and: [{{ ProfileId: '{profileId}' }}, {{ AccountId: '{accountId}' }}]";
            var match = new BsonDocument 
            { 
                { 
                    "$match", 
                    new BsonDocument 
                    {
                        {
                            "$and", 
                            new BsonArray
                            {
                                new BsonDocument{{ "ProfileId", $"'{profileId}'" }}, 
                                new BsonDocument{{ "AccountId", $"'{accountId}'" }}
                            }
                        } 
                    } 
                }
            };

            return new []{match};
        }
    }
}