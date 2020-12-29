using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Approvals.Managers
{
    public class ApprovalsManager : IApprovalsManager
    {
        private readonly IApprovalsService _approvalsService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILicenseManager _licenseManager;

        public ApprovalsManager(
            ILogger<ApprovalsManager> logger,
            IApprovalsService approvalsService,
            IPublishEndpoint publishEndpoint,
            ILicenseManager licenseManager
        )
        {
            _approvalsService = approvalsService;
            _publishEndpoint = publishEndpoint;
            _licenseManager = licenseManager;
        }

        public async Task<IAccountModel?> EvaluateAccountAsync(IAccountModel accountCreatedEvent)
        {
            if (
                string.IsNullOrEmpty(accountCreatedEvent.ProfileId) ||
                string.IsNullOrEmpty(accountCreatedEvent.Id)
            )
                return null;

            var isAllowed = await _licenseManager.EvaluateByUserLicenseScope(accountCreatedEvent);

            if (isAllowed)
                accountCreatedEvent.SetApproval();
            else
                accountCreatedEvent.SetDenial();

            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCreatedEvent.ToApprovalEntity()
            );

            if (approvedEntity != null)
            {
                var accountApprovedEvent = approvedEntity.ToAccountModel<AccountApprovedEvent>(accountCreatedEvent);
                await _publishEndpoint.Publish(accountApprovedEvent);

                return accountApprovedEvent;
            }

            return null;
        }
    }
}