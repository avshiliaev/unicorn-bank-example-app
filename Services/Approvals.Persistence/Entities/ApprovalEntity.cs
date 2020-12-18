using System;
using Sdk.Persistence.Interfaces;

namespace Approvals.Persistence.Entities
{
    public class ApprovalEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Version { get; set; }
    }
}