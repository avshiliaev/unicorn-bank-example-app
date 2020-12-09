using Sdk.Api.Interfaces;

namespace Sdk.Api.Events
{
    public class AccountUpdatedEvent : IAccountModel
    {
        public int Version { get; set; }
        public string Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public string Status { get; set; }
    }
}