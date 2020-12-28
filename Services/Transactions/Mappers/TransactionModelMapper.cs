using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
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
                
                // Properties
                Amount = transactionModel.Amount,
                Info = transactionModel.Info,
                
                // Approvable
                Approved = transactionModel.Approved,
                Pending = transactionModel.Pending,
                
                // Concurrent
                SequentialNumber = transactionModel.SequentialNumber
            };
        }
    }
}