using Sdk.Api.Interfaces;

namespace Sdk.Api.Validators
{
    public static class TransactionValidator
    {
        public static bool IsValid(this ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId) ||
                string.IsNullOrEmpty(transactionModel.Id)
            )
                return false;
            return true;
        }
    }
}