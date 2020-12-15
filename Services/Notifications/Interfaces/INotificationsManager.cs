using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Notifications.Interfaces
{
    public interface INotificationsManager
    {
        NotificationDto AddFromAccount(IAccountModel accountModel);
        NotificationDto AddFromTransaction(ITransactionModel transactionModel);
    }
}