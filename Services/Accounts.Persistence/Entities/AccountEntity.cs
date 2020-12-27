using System;
using Sdk.Persistence.Interfaces;

namespace Accounts.Persistence.Entities
{
    public class AccountEntity : IEntity
    {
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public int LastTransactionNumber { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}