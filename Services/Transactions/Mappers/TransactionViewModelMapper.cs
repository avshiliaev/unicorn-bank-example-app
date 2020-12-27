using System;
using System.Globalization;
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
                AccountId = transactionViewModel.AccountId,
                ProfileId = profileId,
                Amount = transactionViewModel.Amount,
                Info = transactionViewModel.Info,
                Approved = false,
                Pending = true,
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                SequentialNumber = 0
            };
        }
    }
}