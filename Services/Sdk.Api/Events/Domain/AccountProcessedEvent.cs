using Sdk.Interfaces;

namespace Sdk.Api.Events.Domain
{
    public class AccountProcessedEvent : IAccountModel, IEvent
    {
        // Common
        public string Id { get; set; }
        public string EntityId { get; set; }
        public int Version { get; set; }

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

        // Properties
        public float Balance { get; set; }

        // Foreign
        public string ProfileId { get; set; }

        // Approvable 
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }
    }
}