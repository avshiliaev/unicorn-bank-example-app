using System.Threading.Tasks;
using Billings.Interfaces;
using Billings.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Billings.Managers
{
    public class BillingsManager : IBillingsManager
    {
        private readonly IBillingsService _billingsService;
        private readonly ILicenseManager<ITransactionModel> _licenseManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public BillingsManager(
            ILogger<BillingsManager> logger,
            IBillingsService billingsService,
            ILicenseManager<ITransactionModel> licenseManager,
            IPublishEndpoint publishEndpoint
        )
        {
            _billingsService = billingsService;
            _licenseManager = licenseManager;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<ITransactionModel?> EvaluateTransactionAsync(ITransactionModel transactionCreatedEvent)
        {
            if (
                string.IsNullOrEmpty(transactionCreatedEvent.ProfileId) ||
                string.IsNullOrEmpty(transactionCreatedEvent.AccountId)
            )
                return null;

            var isTransactionAllowed = await _licenseManager.EvaluateNewEntityAsync(transactionCreatedEvent);

            if (isTransactionAllowed)
                transactionCreatedEvent.SetApproval();
            else
                transactionCreatedEvent.SetDenial();

            var processedEntity = await _billingsService.CreateBillingAsync(
                transactionCreatedEvent.ToBillingEntity()
            );

            if (processedEntity != null)
            {
                var transactionProcessedEvent = processedEntity.ToTransactionModel<TransactionIsCheckedEvent>(
                    transactionCreatedEvent
                );
                await _publishEndpoint.Publish(transactionProcessedEvent);

                return transactionProcessedEvent;
            }

            return null;
        }
    }
}