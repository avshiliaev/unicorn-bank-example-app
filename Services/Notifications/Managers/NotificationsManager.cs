using Microsoft.Extensions.Logging;
using Notifications.Interfaces;
using Notifications.Mappers;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Notifications.Managers
{
    public class NotificationsManager : INotificationsManager
    {
        private ILogger<NotificationsManager> _logger;
        private readonly INotificationsService _notificationsService;

        public NotificationsManager(
            ILogger<NotificationsManager> logger,
            INotificationsService notificationsService
        )
        {
            _logger = logger;
            _notificationsService = notificationsService;
        }

        public NotificationDto AddFromAccount(IAccountModel accountModel)
        {
            var notification = _notificationsService.Create(accountModel.ToNotificationEntity());
            return notification.ToNotificationsModel<NotificationDto>();
        }

        public NotificationDto AddFromTransaction(ITransactionModel transactionModel)
        {
            var notification = _notificationsService.Create(transactionModel.ToNotificationEntity());
            return notification.ToNotificationsModel<NotificationDto>();
        }
    }
}