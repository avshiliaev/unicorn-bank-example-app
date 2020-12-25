using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Transactions.Interfaces;

namespace Transactions.Handlers
{
    public class TransactionsSubscriptionsHandler : IConsumer<TransactionProcessedEvent>
    {
        private readonly ILogger<TransactionsSubscriptionsHandler> _logger;
        private readonly ITransactionsManager _transactionsManager;

        public TransactionsSubscriptionsHandler(
            ILogger<TransactionsSubscriptionsHandler> logger,
            ITransactionsManager transactionsManager
        )
        {
            _logger = logger;
            _transactionsManager = transactionsManager;
        }

        public TransactionsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<TransactionProcessedEvent> context)
        {
            _logger.LogDebug($"Received new TransactionProcessedEvent for {context.Message.Id}");
            await _transactionsManager.UpdateStatusOfTransactionAsync(context.Message);
        }
    }
}