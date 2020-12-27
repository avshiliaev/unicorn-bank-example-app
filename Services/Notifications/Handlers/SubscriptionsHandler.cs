using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Notifications.Interfaces;
using Sdk.Api.Events;

namespace Notifications.Handlers
{
    public class NotificationsSubscriptionsHandler : IConsumer<NotificationEvent>
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

        // TODO: Does ? change the signature?
        public Task? Consume(ConsumeContext<NotificationEvent> context)
        {
            _logger.LogDebug($"Received new NotificationEvent for {context.Message.Id}");
            var notificationDto = _notificationsManager.AddNewNotification(context.Message);

            if (notificationDto != null)
                return Task.CompletedTask;
            return null;
        }
    }
}