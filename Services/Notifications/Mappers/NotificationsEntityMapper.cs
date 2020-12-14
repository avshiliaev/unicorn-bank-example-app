using System;
using Notifications.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Notifications.Mappers
{
    public static class NotificationsEntityMapper
    {
        public static TModel ToNotificationsModel<TModel>(this NotificationEntity notificationEntity)
            where TModel : class, INotificationModel, new()
        {
            return new TModel
            {
                Id = notificationEntity.Id.ToGuid(),
                Description = notificationEntity.Description,
                ProfileId = notificationEntity.ProfileId.ToGuid(),
                Status = notificationEntity.Status,
                TimeStamp = DateTime.Parse(notificationEntity.TimeStamp),
                Title = notificationEntity.Title,
                Version = notificationEntity.Version
            };
        }
    }
}