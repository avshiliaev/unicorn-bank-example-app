using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Profiles.Persistence.Entities
{
    public class TransactionSubEntity : IApprovable
    {
        // Properties
        public float Amount { get; set; }
        public string Info { get; set; }

        // Concurrency
        public int SequentialNumber { get; set; }

        // Common entity
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }
    }

    public class ProfileEntity : IMongoEntity, IApprovable, IConcurrentHost
    {
        // Properties
        public float Balance { get; set; }
        public List<TransactionSubEntity> Transactions { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

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