using System.Threading.Tasks;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;

namespace Accounts.Handlers
{
    public class SubscriptionsHandler
        : IConsumer<AccountApprovedEvent>, IConsumer<TransactionCreatedEvent>
    {
        private readonly ILogger<SubscriptionsHandler> _logger;
        private IAccountsManager _accountsManager;

        public SubscriptionsHandler(
            ILogger<SubscriptionsHandler> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public SubscriptionsHandler()
        {
        }

        /*
         * The Consume method returns a Task that is awaited by MassTransit. While the consumer method is executing,
         * the message is unavailable to other receive endpoints. If the Task completes successfully, the message is
         * acknowledged and removed from the queue.
         */
        public Task Consume(ConsumeContext<AccountApprovedEvent> context)
        {
            _logger.LogDebug($"-------> AccountDto {context}");
            return Task.CompletedTask;
        }

        public Task Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            _logger.LogDebug($"-------> TransactionDto {context}");
            return Task.CompletedTask;
        }
    }
}