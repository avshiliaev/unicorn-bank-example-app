using System;
using Sdk.Persistence.Interfaces;

namespace Billings.Persistence.Entities
{
    public class BillingEntity : IEntity
    {
        public Guid Id { get; set; }
        public Guid TransactionId { get; set; }
        public bool Approved { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}