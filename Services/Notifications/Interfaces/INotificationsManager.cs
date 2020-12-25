using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Notifications.Interfaces
{
    public interface INotificationsManager
    {
        NotificationDto AddNewNotification(INotificationModel notificationModel);
    }
}