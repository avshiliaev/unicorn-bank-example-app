using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
{
    public static class TransactionModelMapper
    {
        public static T ToTransactionModel<T>(this ITransactionsContext transactionsContext)
            where T : class, ITransactionModel, new()
        {
            return new T
            {
                // Common
                Id = transactionsContext.Id,
                Version = transactionsContext.Version,

                // Foreign
                AccountId = transactionsContext.AccountId,
                ProfileId = transactionsContext.ProfileId,

                // Properties
                Amount = transactionsContext.Amount,
                Info = transactionsContext.Info,

                // Approvable
                Approved = transactionsContext.Approved,
                Pending = transactionsContext.Pending,
                Blocked = transactionsContext.Blocked,

                // Concurrent
                SequentialNumber = transactionsContext.SequentialNumber
            };
        }

        public static TransactionEntity ToTransactionEntity(this ITransactionModel transactionModel)
        {
            return new TransactionEntity
            {
                // Common
                Id = transactionModel.Id.ToGuid(),
                Version = transactionModel.Version,

                // Foreign
                AccountId = transactionModel.AccountId.ToGuid(),
                ProfileId = transactionModel.ProfileId,

                // Properties
                Amount = transactionModel.Amount,
                Info = transactionModel.Info,

                // Approvable
                Approved = transactionModel.Approved,
                Pending = transactionModel.Pending,
                Blocked = transactionModel.Blocked,

                // Concurrent
                SequentialNumber = transactionModel.SequentialNumber
            };
        }
    }
}