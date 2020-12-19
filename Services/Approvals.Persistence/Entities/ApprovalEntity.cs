using System;
using Sdk.Persistence.Interfaces;

namespace Approvals.Persistence.Entities
{
    public class ApprovalEntity : IEntity
    {
        public Guid AccountId { get; set; }
        public bool Approved { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}