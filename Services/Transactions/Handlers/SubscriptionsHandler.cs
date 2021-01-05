using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Extensions;
using Transactions.Interfaces;

namespace Transactions.Handlers
{
    public class TransactionsSubscriptionsHandler : 
        IConsumer<TransactionIsCheckedEvent>, 
        IConsumer<AccountUpdatedEvent>
    {
        private readonly ILogger<TransactionsSubscriptionsHandler> _logger;
        private readonly ITransactionsManager _transactionsManager;
        private readonly IAccountsManager _accountsManager;

        public TransactionsSubscriptionsHandler(
            ILogger<TransactionsSubscriptionsHandler> logger,
            ITransactionsManager transactionsManager,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _transactionsManager = transactionsManager;
            _accountsManager = accountsManager;
        }

        public TransactionsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<TransactionIsCheckedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionProcessedEvent for {context.Message.Id}");
            var result = await _transactionsManager.ProcessTransactionCheckedEventAsync(context.Message);

            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
        
        public async Task Consume(ConsumeContext<AccountUpdatedEvent> context)
        {
            _logger.LogDebug($"Received new AccountUpdatedEvent for {context.Message.Version}");
            var result = await _accountsManager.ProcessTransactionCheckedEventAsync(context.Message);
            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}