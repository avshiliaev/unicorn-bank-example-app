using System;
using System.Threading.Tasks;
using Billings.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;

namespace Billings.Handlers
{
    public class BillingsSubscriptionsHandler : IConsumer<TransactionCheckCommand>
    {
        private readonly IBillingsManager _billingsManager;
        private readonly ILogger<BillingsSubscriptionsHandler> _logger;

        public BillingsSubscriptionsHandler(
            ILogger<BillingsSubscriptionsHandler> logger,
            IBillingsManager billingsManager
        )
        {
            _logger = logger;
            _billingsManager = billingsManager;
        }

        public BillingsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<TransactionCheckCommand> context)
        {
            _logger.LogDebug($"Received new TransactionCreatedEvent for {context.Message.Id}");
            var result = await _billingsManager.EvaluateTransactionAsync(context.Message);

            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}