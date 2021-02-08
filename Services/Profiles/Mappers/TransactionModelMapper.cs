using Profiles.Persistence.Entities;
using Sdk.Interfaces;

namespace Profiles.Mappers
{
    public static class TransactionModelMapper
    {
        public static TransactionSubEntity ToTransactionSubEntity(this ITransactionModel transactionModel)
        {
            return new TransactionSubEntity
            {
                // Common
                Id = transactionModel.Id,
                Version = transactionModel.Version,

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