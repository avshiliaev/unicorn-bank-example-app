using System;
using System.Globalization;
using Notifications.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Notifications.Mappers
{
    public static class TransactionModelMapper
    {
        public static NotificationEntity ToNotificationEntity(this ITransactionModel transactionModel)
        {
            return new NotificationEntity
            {
                Description = "Your transaction has been processed",
                ProfileId = transactionModel.ProfileId,
                Status = transactionModel.Approved.ToString(),
                TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Title = "Title",
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = transactionModel.Version
            };
        }
    }
}