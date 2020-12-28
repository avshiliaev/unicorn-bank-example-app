using System;
using System.Globalization;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Mappers
{
    public static class TransactionEntityMapper
    {
        public static T ToTransactionModel<T>(this TransactionEntity transactionEntity)
            where T : class, ITransactionModel, IDataModel, IConcurrent, IApprovable, new()
        {
            return new T
            {
                // Common
                Id = transactionEntity.Id.ToString(),
                Version = transactionEntity.Version,

                // Foreign
                AccountId = transactionEntity.AccountId.ToString(),
                ProfileId = transactionEntity.ProfileId,

                // Properties
                Amount = transactionEntity.Amount,
                Info = transactionEntity.Info,
                Timestamp = transactionEntity.Created.ToString(CultureInfo.InvariantCulture),

                // Approvable
                Approved = transactionEntity.Approved,
                Pending = transactionEntity.Pending,

                // Concurrent
                SequentialNumber = transactionEntity.SequentialNumber
            };
        }

        public static NotificationEvent ToNotificationEvent(this TransactionEntity transactionEntity)
        {
            return new NotificationEvent
            {
                Description =
                    $"Your transaction has been {(transactionEntity.Approved ? "processed" : "declined")}.",
                ProfileId = transactionEntity.ProfileId,
                Status = transactionEntity.Approved ? "processed" : "declined",
                TimeStamp = DateTime.Now,
                Title = $"{(transactionEntity.Approved ? "Processing" : "Denial")}",
                Id = Guid.NewGuid()
            };
        }
    }
}