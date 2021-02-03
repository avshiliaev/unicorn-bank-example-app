using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Accounts.Mappers
{
    public static class TransactionModelMapper
    {
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
                TransactionId = transactionModel.TransactionId,

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