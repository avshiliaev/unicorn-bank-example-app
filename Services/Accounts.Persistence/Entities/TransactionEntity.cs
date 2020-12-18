using System;
using Sdk.Persistence.Interfaces;

namespace Accounts.Persistence.Entities
{
    public class TransactionEntity : IEntity
    {
        public Guid AccountId { get; set; }
        public float Amount { get; set; }
        public bool Approved { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}