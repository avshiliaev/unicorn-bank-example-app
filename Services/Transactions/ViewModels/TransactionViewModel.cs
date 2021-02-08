using Sdk.Api.Attributes;

namespace Transactions.ViewModels
{
    public class TransactionViewModel
    {
        [IsValidGuid] public string AccountId { get; set; } = null!;
        public float Amount { get; set; }
        public string Info { get; set; } = null!;
    }
}