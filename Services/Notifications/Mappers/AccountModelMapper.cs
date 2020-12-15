using System;
using System.Globalization;
using Notifications.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Notifications.Mappers
{
    public static class AccountModelMapper
    {
        public static NotificationEntity ToNotificationEntity(this IAccountModel accountModel)
        {
            return new NotificationEntity
            {
                Description = "Your account has been approved",
                ProfileId = accountModel.ProfileId,
                Status = accountModel.Approved.ToString(),
                TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Title = "Title",
                Id = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = accountModel.Version
            };
        }
    }
}