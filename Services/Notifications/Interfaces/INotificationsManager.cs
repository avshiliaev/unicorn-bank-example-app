using Sdk.Api.Dto;
using Sdk.Interfaces;

namespace Notifications.Interfaces
{
    public interface INotificationsManager
    {
        NotificationDto? AddNewNotification(INotificationModel notificationModel);
    }
}