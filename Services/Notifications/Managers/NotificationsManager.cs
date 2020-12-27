using Microsoft.Extensions.Logging;
using Notifications.Interfaces;
using Notifications.Mappers;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Notifications.Managers
{
    public class NotificationsManager : INotificationsManager
    {
        private readonly INotificationsService _notificationsService;
        private ILogger<NotificationsManager> _logger;

        public NotificationsManager(
            ILogger<NotificationsManager> logger,
            INotificationsService notificationsService
        )
        {
            _logger = logger;
            _notificationsService = notificationsService;
        }

        public NotificationDto? AddNewNotification(INotificationModel notificationModel)
        {
            if (string.IsNullOrEmpty(notificationModel.ProfileId))
                return null;
            var notification = _notificationsService.Create(notificationModel.ToNotificationsEntity());
            return notification.ToNotificationsModel<NotificationDto>();
        }
    }
}