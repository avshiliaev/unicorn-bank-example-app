using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.Events
{
    public class AccountCreatedEvent : IAccountModel, IEvent
    {
        public int Version { get; set; }
        public string Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public int LastTransactionNumber { get; set; }
    }
}