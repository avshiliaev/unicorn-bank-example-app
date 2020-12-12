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
                Id = transactionModel.Id.ToGuid(),
                Amount = transactionModel.Amount,
                AccountId = transactionModel.AccountId.ToGuid(),
                Approved = transactionModel.Approved,
                Version = transactionModel.Version
            };
        }
    }
}