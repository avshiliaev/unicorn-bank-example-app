using Sdk.Api.Interfaces;

namespace Sdk.Api.Events
{
    public class TransactionCreatedEvent : ITransactionModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public float Amount { get; set; }
        public string Info { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
        public int Version { get; set; }
    }
}