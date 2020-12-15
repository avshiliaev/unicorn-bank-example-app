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
                Id = notificationModel.Id.ToString(),
                Description = notificationModel.Description,
                ProfileId = notificationModel.ProfileId.ToString(),
                Status = notificationModel.Status,
                TimeStamp = notificationModel.TimeStamp.ToString(CultureInfo.InvariantCulture),
                Title = notificationModel.Title,
                Version = notificationModel.Version
            };
        }
    }
}