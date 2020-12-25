using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.Events
{
    public class TransactionUpdatedEvent : ITransactionModel, IEvent
    {
        public int Version { get; set; }
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string ProfileId { get; set; }
        public float Amount { get; set; }
        public string Info { get; set; }
        public bool Approved { get; set; }
        public string Timestamp { get; set; }
    }
}