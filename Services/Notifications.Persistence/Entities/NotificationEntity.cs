using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sdk.Persistence.Interfaces;

namespace Notifications.Persistence.Entities
{
    public class NotificationEntity : IMongoEntity
    {
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("profile")]
        public string ProfileId { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("time")]
        public string TimeStamp { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("guid")]
        public string GlobalId { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}