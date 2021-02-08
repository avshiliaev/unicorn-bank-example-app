using System;
using System.Globalization;
using Notifications.Persistence.Entities;
using Sdk.Interfaces;

namespace Notifications.Mappers
{
    public static class NotificationsEntityMapper
    {
        public static TModel ToNotificationsModel<TModel>(this NotificationEntity notificationEntity)
            where TModel : class, INotificationModel, new()
        {
            return new TModel
            {
                Id = notificationEntity.Id,
                Description = notificationEntity.Description,
                ProfileId = notificationEntity.ProfileId,
                Status = notificationEntity.Status,
                TimeStamp = DateTime.Parse(notificationEntity.TimeStamp, CultureInfo.InvariantCulture),
                Title = notificationEntity.Title,
                Version = notificationEntity.Version
            };
        }
    }
}