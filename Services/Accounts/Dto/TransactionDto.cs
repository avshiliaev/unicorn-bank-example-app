using Sdk.Api.Interfaces;

namespace Accounts.Dto
{
    public class TransactionDto : ITransactionModel
    {
        public int Version { get; set; }
    }
}