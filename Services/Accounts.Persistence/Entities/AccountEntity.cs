using System;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Persistence.Entities
{
    public class AccountEntity : IEntity, IApprovable, IConcurrentHost
    {
        // Properties
        public float Balance { get; set; }

        // Foreign Properties
        public string ProfileId { get; set; }
        public string AccountId { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

        // Common Entity
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}