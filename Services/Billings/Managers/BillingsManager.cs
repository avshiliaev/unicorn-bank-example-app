using System.Linq;
using System.Threading.Tasks;
using Billings.Interfaces;
using Billings.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Concurrency.Extensions;
using Sdk.Extensions;

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
            
            var allTransactions = await _billingsService.GetManyByParameterAsync(
                b => b!.Approved && b.AccountId == transactionCreatedEvent.AccountId.ToGuid()
            );
            var lastTransactionNumber = allTransactions.Max(t => t?.Amount);

            transactionCreatedEvent.SetApproval();
            
            var approvedEntity = await _billingsService.CreateBillingAsync(
                transactionCreatedEvent.ToBillingEntity()
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