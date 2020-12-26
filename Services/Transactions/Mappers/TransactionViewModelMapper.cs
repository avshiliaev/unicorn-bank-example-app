using Sdk.Api.Interfaces;
using Transactions.ViewModels;

namespace Transactions.Mappers
{
    public static class TransactionViewModelMapper
    {
        public static T ToTransactionModel<T>(this TransactionViewModel transactionViewModel, string profileId)
            where T : class, ITransactionModel, new()
        {
            return new T
            {
                Amount = transactionViewModel.Amount,
                AccountId = transactionViewModel.AccountId.ToString(),
                ProfileId = profileId,
                Approved = false,
                Version = 0
            };
        }
    }
}