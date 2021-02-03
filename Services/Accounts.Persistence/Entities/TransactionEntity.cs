using System;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Persistence.Entities
{
    public class TransactionEntity : IEntity, IConcurrent, IApprovable
    {
        // Foreign Properties
        public Guid AccountId { get; set; }
        public string ProfileId { get; set; }
        public string TransactionId { get; set; }

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
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}