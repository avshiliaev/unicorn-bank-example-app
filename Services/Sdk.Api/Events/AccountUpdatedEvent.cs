using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.Events
{
    public class AccountUpdatedEvent : IAccountModel, IEvent
    {
        public int Version { get; set; }
        public string Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public bool Approved { get; set; }
    }
}