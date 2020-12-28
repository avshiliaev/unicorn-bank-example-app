using System.Threading.Tasks;
using Billings.Interfaces;
using Billings.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Billings.Managers
{
    public class BillingsManager : IBillingsManager
    {
        private readonly IBillingsService _billingsService;
        private readonly ILicenseManager _licenseManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public BillingsManager(
            ILogger<BillingsManager> logger,
            IBillingsService billingsService,
            ILicenseManager licenseManager,
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

            var isUserAllowed = await _licenseManager.EvaluateByUserLicenseScope(transactionCreatedEvent);

            if (isUserAllowed)
                transactionCreatedEvent.SetApproval();
            else
                transactionCreatedEvent.SetDenial();

            var processedEntity = await _billingsService.CreateBillingAsync(
                transactionCreatedEvent.ToBillingEntity()
            );

            if (processedEntity != null)
            {
                var transactionProcessedEvent = processedEntity.ToTransactionModel<TransactionProcessedEvent>(
                    transactionCreatedEvent
                );
                await _publishEndpoint.Publish(transactionProcessedEvent);

                return transactionProcessedEvent;
            }

            return null;
        }
    }
}