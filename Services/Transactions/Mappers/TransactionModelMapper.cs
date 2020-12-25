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
                Id = transactionModel.Id.ToGuid(),
                Amount = transactionModel.Amount,
                AccountId = transactionModel.AccountId.ToGuid(),
                ProfileId = transactionModel.ProfileId,
                Approved = transactionModel.Approved,
                Version = transactionModel.Version
            };
        }
    }
}