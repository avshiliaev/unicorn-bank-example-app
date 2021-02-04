using System;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Approvals.Persistence.Entities
{
    public class AccountEntity : IEventRecord, IApprovable, IConcurrentHost
    {
        // Properties
        public float Balance { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

        // Common Entity
        public string Id { get; set; }
        public string EntityId { get; set; }
        public string ProfileId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}