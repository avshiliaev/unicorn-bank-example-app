using Sdk.Interfaces;

namespace Sdk.Api.Validators
{
    public static class TransactionValidator
    {
        public static bool IsValid(this ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId) ||
                string.IsNullOrEmpty(transactionModel.EntityId)
            )
                return false;
            return true;
        }
    }
}