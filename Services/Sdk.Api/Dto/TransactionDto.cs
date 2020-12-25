using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class TransactionDto : ITransactionModel
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