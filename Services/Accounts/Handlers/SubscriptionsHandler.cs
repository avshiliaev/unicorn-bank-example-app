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

        // TODO: Register handlers as subscribers!
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

        public async Task Consume(ConsumeContext<AccountApprovedEvent> context)
        {
            _logger.LogDebug($"-------> AccountDto {context}");
        }

        public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            _logger.LogDebug($"-------> TransactionDto {context}");
        }
    }
}