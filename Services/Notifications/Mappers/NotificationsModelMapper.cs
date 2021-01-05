using System;
using System.Globalization;
using Notifications.Persistence.Entities;
using Sdk.Api.Interfaces;

namespace Notifications.Mappers
{
    public static class NotificationsModelMapper
    {
        public static NotificationEntity ToNotificationsEntity(this INotificationModel notificationModel)
        {
            return new NotificationEntity
            {
                Description = notificationModel.Description,
                ProfileId = notificationModel.ProfileId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Status = notificationModel.Status,
                TimeStamp = notificationModel.TimeStamp.ToString(CultureInfo.InvariantCulture),
                Title = notificationModel.Title,
                Version = notificationModel.Version
            };
        }
    }
}