using System;
using Sdk.Persistence.Interfaces;

namespace Transactions.Persistence.Entities
{
    public class TransactionEntity : IEntity
    {
        public Guid AccountId { get; set; }
        public string ProfileId { get; set; }
        public float Amount { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public string Info { get; set; }
        public int SequentialNumber { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}