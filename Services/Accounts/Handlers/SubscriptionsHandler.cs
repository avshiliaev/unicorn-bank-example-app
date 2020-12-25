using System.Threading.Tasks;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;

namespace Accounts.Handlers
{
    public class AccountsSubscriptionsHandler
        : IConsumer<AccountApprovedEvent>, IConsumer<TransactionCreatedEvent>
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILogger<AccountsSubscriptionsHandler> _logger;

        public AccountsSubscriptionsHandler(
            ILogger<AccountsSubscriptionsHandler> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public AccountsSubscriptionsHandler()
        {
        }

        /*
         * The Consume method returns a Task that is awaited by MassTransit. While the consumer method is executing,
         * the message is unavailable to other receive endpoints. If the Task completes successfully, the message is
         * acknowledged and removed from the queue.
         */
        public async Task Consume(ConsumeContext<AccountApprovedEvent> context)
        {
            _logger.LogDebug($"Received new AccountApprovedEvent for {context.Message.Id}");
            await _accountsManager.AddApprovalToAccountAsync(context.Message);
        }

        public async Task Consume(ConsumeContext<TransactionCreatedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionCreatedEvent for {context.Message.Version}");
            await _accountsManager.AddTransactionToAccountAsync(context.Message);
        }
    }
}