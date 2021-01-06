namespace Sdk.Persistence.Extensions
{
    public static class MongoDbPipelineBuilder
    {
        public static string ToMongoPipelineMatchByProfileId(this string profileId)
        {
            var pipeline = @"{ ProfileId: " + profileId + " }";
            return pipeline;
        }
        
        /*
         *
         *                    { 
                     $match: {
                          $and: [ 
                              {type: {$in: ["TOYS"]}}, 
                              {type: {$nin: ["BARBIE"]}}, 
                              {time: {$lt:ISODate("2013-12-09T00:00:00Z")}}
                          ]
                     }
                   }
         */
        public static string ToMongoPipelineMatchByAccountId(this string accountId)
        {
            var pipeline = @"{ AccountId: " + accountId + " }";
            return pipeline;
        }
    }
}