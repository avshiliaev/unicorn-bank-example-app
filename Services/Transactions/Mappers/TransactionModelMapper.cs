using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
{
    public static class TransactionModelMapper
    {
        public static TransactionEntity ToTransactionEntity(this ITransactionModel transactionModel, int number)
        {
            return new TransactionEntity
            {
                Id = transactionModel.Id.ToGuid(),
                AccountId = transactionModel.AccountId.ToGuid(),
                ProfileId = transactionModel.ProfileId,
                Amount = transactionModel.Amount,
                Info = transactionModel.Info,
                Approved = transactionModel.Approved,
                Pending = transactionModel.Pending,
                SequentialNumber = number
            };
        }
    }
}