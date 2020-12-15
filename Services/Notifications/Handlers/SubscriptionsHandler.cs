using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notifications.Interfaces;
using Sdk.Api.Events;

namespace Notifications.Handlers
{
    public class NotificationsSubscriptionsHandler
        :
            IConsumer<AccountApprovedEvent>,
            IConsumer<TransactionProcessedEvent>
    {
        private readonly ILogger<NotificationsSubscriptionsHandler> _logger;
        private readonly INotificationsManager _notificationsManager;

        public NotificationsSubscriptionsHandler(
            ILogger<NotificationsSubscriptionsHandler> logger,
            INotificationsManager notificationsManager
        )
        {
            _logger = logger;
            _notificationsManager = notificationsManager;
        }

        public NotificationsSubscriptionsHandler()
        {
        }

        public Task Consume(ConsumeContext<AccountApprovedEvent> context)
        {
            _logger.LogDebug($"Received new AccountApprovedEvent for {context.Message.Id}");
            var notificationDto = _notificationsManager.AddFromAccount(context.Message);

            if (notificationDto != null)
                return Task.CompletedTask;
            return null;
        }

        public Task Consume(ConsumeContext<TransactionProcessedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionProcessedEvent for {context.Message.Version}");
            var notificationDto = _notificationsManager.AddFromTransaction(context.Message);

            if (notificationDto != null)
                return Task.CompletedTask;
            return null;
        }
    }
}