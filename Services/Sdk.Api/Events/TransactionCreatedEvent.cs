using Sdk.Api.Interfaces;

namespace Sdk.Api.Events
{
    public class TransactionCreatedEvent : ITransactionModel
    {
        public int Version { get; set; }
    }
}