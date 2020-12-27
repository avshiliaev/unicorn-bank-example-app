using System.Globalization;
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
                AccountId = transactionEntity.AccountId.ToString(),
                ProfileId = transactionEntity.ProfileId,
                Amount = transactionEntity.Amount,
                Info = transactionEntity.Info,
                Approved = transactionEntity.Approved,
                Pending = transactionEntity.Pending,
                Timestamp = transactionEntity.Created.ToString(CultureInfo.InvariantCulture),
                SequentialNumber = transactionEntity.SequentialNumber
            };
        }
    }
}