using System;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Billings.Persistence.Entities
{
    public class BillingEntity : IEntity, IConcurrent, IApprovable
    {
        // Foreign Properties
        public string ProfileId { get; set; }
        public Guid AccountId { get; set; }
        public Guid TransactionId { get; set; }

        // Properties
        public float Amount { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }

        // Concurrency
        public int SequentialNumber { get; set; }

        // Common Entity
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}