using System;
using Sdk.Interfaces;

namespace Transactions.Persistence.Entities
{
    public class TransactionEntity : IRecord, IConcurrent, IApprovable
    {
        // Foreign Properties
        public string AccountId { get; set; }

        // Properties
        public float Amount { get; set; }
        public string Info { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrency
        public int SequentialNumber { get; set; }

        // Common Entity
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string ProfileId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}