using System;
using Accounts.Persistence.Entities;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Accounts.Mappers
{
    public static class AccountEntityMapper
    {
        public static T ToAccountModel<T>(this AccountEntity accountEntity)
            where T : class, IAccountModel, new()
        {
            return new T
            {
                Id = accountEntity.Id.ToString(),
                Version = accountEntity.Version,

                Balance = accountEntity.Balance,

                ProfileId = accountEntity.ProfileId,

                Approved = accountEntity.Approved,
                Pending = accountEntity.Pending,
                Blocked = accountEntity.Blocked,

                LastSequentialNumber = accountEntity.LastSequentialNumber
            };
        }

        public static T ToAccountEvent<T>(this AccountEntity accountEntity)
            where T : class, IAccountModel, IEvent, new()
        {
            return new T
            {
                Id = accountEntity.Id.ToString(),
                Version = accountEntity.Version,

                Balance = accountEntity.Balance,

                ProfileId = accountEntity.ProfileId,

                Approved = accountEntity.Approved,
                Pending = accountEntity.Pending,
                Blocked = accountEntity.Blocked,

                LastSequentialNumber = accountEntity.LastSequentialNumber
            };
        }

        public static NotificationEvent ToNotificationEvent(this AccountEntity accountEntity)
        {
            return new NotificationEvent
            {
                Description = $"Your account has been {(accountEntity.Approved ? "approved" : "declined")}.",
                ProfileId = accountEntity.ProfileId,
                Status = accountEntity.Approved ? "approved" : "declined",
                TimeStamp = DateTime.Now,
                Title = $"{(accountEntity.Approved ? "Approval" : "Denial")}",
                Id = Guid.NewGuid()
            };
        }

        public static AccountEntity SetBalance(this AccountEntity accountEntity, TransactionEntity transactionEntity)
        {
            accountEntity.Balance += transactionEntity.Amount;
            return accountEntity;
        }
    }
}