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
                // Common
                Version = 0,

                // Foreign
                AccountId = transactionViewModel.AccountId,
                ProfileId = profileId,

                // Properties
                Amount = transactionViewModel.Amount,
                Info = transactionViewModel.Info,
                Timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),

                // Approvable
                Approved = false,
                Pending = true,

                // Concurrent
                SequentialNumber = 1
            };
        }
    }
}