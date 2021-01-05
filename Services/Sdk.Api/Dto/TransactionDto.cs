using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class TransactionDto : ITransactionModel
    {
        public string Id { get; set; }
        public int Version { get; set; }

        // Foreign
        public string AccountId { get; set; }
        public string ProfileId { get; set; }

        // Properties
        public float Amount { get; set; }
        public string Info { get; set; }
        public string Timestamp { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent
        public int SequentialNumber { get; set; }
    }
}