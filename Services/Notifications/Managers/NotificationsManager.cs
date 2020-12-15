using System;
using System.Globalization;
using System.Threading.Tasks;
using Notifications.Interfaces;
using Notifications.Mappers;
using Notifications.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Notifications.Managers
{
    public class NotificationsManager : INotificationsManager
    {
        private INotificationsService _notificationsService;

        public NotificationsManager(INotificationsService notificationsService)
        {
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

    public interface INotificationsManager
    {
        NotificationDto AddFromAccount(IAccountModel accountModel);
        NotificationDto AddFromTransaction(ITransactionModel transactionModel);
    }
}