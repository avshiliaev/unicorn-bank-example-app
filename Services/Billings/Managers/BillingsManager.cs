using System.Threading;
using System.Threading.Tasks;
using Billings.Interfaces;
using Billings.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;

namespace Billings.Managers
{
    public class BillingsManager : IBillingsManager
    {
        private readonly IBillingsService _billingsService;
        private readonly IPublishEndpoint _publishEndpoint;

        public BillingsManager(
            ILogger<BillingsManager> logger,
            IBillingsService billingsService,
            IPublishEndpoint publishEndpoint
        )
        {
            _billingsService = billingsService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ITransactionModel> EvaluateTransactionAsync(ITransactionModel transactionCreatedEvent)
        {
            Thread.Sleep(5000);
            var approval = true;
            var approvedEntity = await _billingsService.CreateBillingAsync(
                transactionCreatedEvent.ToBillingEntity(approval)
            );
            var transactionApprovedEvent = approvedEntity.ToTransactionModel<TransactionProcessedEvent>();
            await _publishEndpoint.Publish(transactionApprovedEvent);

            return transactionCreatedEvent;
        }
    }
}