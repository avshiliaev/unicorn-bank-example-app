using System;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Approvals.Persistence.Entities
{
    public class ApprovalEntity : IEntity, IApprovable
    {
        // Foreign Properties
        public Guid AccountId { get; set; }
        public string ProfileId { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }

        // Common Entity
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}