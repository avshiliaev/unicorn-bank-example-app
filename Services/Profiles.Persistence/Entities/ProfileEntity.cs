using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sdk.Persistence.Interfaces;

namespace Profiles.Persistence.Entities
{
    public class ProfileEntity : IMongoEntity
    {
        // Properties
        public string Description { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
        public string Title { get; set; }

        // Foreign Properties
        public string ProfileId { get; set; }

        // Common entity
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}