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

        public async Task<ITransactionModel?> EvaluateTransactionAsync(ITransactionModel transactionCreatedEvent)
        {
            if (
                string.IsNullOrEmpty(transactionCreatedEvent.ProfileId) ||
                string.IsNullOrEmpty(transactionCreatedEvent.AccountId)
            )
                return null;

            var approval = true;

            var approvedEntity = await _billingsService.CreateBillingAsync(
                transactionCreatedEvent.ToBillingEntity(approval)
            );

            if (approvedEntity != null)
            {
                var transactionApprovedEvent = approvedEntity.ToTransactionModel<TransactionProcessedEvent>(
                    transactionCreatedEvent
                );
                await _publishEndpoint.Publish(transactionApprovedEvent);

                return transactionApprovedEvent;
            }

            return null;
        }
    }
}