using Sdk.Api.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
{
    public static class TransactionEntityMapper
    {
        public static T ToTransactionModel<T>(this TransactionEntity transactionEntity)
            where T : class, ITransactionModel, new()
        {
            return new T
            {
                Id = transactionEntity.Id.ToString(),
                Amount = transactionEntity.Amount,
                AccountId = transactionEntity.AccountId.ToString(),
                ProfileId = transactionEntity.ProfileId,
                Approved = transactionEntity.Approved,
                Version = transactionEntity.Version
            };
        }
    }
}