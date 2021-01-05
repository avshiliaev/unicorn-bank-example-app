using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Extensions;

namespace Accounts.Handlers
{
    public class AccountsSubscriptionsHandler
        : IConsumer<AccountIsCheckedEvent>, IConsumer<TransactionUpdatedEvent>
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

        public async Task Consume(ConsumeContext<AccountIsCheckedEvent> context)
        {
            _logger.LogDebug($"Received new AccountApprovedEvent for {context.Message.Id}");

            var result = await _accountsManager.ProcessAccountIsCheckedEventAsync(context.Message);
            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionCreatedEvent for {context.Message.Version}");

            if (!context.Message.IsApproved()) return;
            var result = await _accountsManager.ProcessTransactionUpdatedEventAsync(context.Message);
            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}