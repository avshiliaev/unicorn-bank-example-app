using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Approvals.Managers
{
    public class ApprovalsManager : IApprovalsManager
    {
        private readonly IApprovalsService _approvalsService;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private readonly IPublishEndpoint _publishEndpoint;

        public ApprovalsManager(
            ILogger<ApprovalsManager> logger,
            IApprovalsService approvalsService,
            IPublishEndpoint publishEndpoint,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            _approvalsService = approvalsService;
            _publishEndpoint = publishEndpoint;
            _licenseManager = licenseManager;
        }

        public async Task<IAccountModel?> EvaluateAccountPendingAsync(IAccountModel accountCreatedEvent)
        {
            if (
                string.IsNullOrEmpty(accountCreatedEvent.ProfileId) ||
                string.IsNullOrEmpty(accountCreatedEvent.Id)
            )
                return null;

            var isCreationAllowed = await _licenseManager.EvaluateNewEntityAsync(accountCreatedEvent);

            if (isCreationAllowed)
                accountCreatedEvent.SetApproval();
            else
                accountCreatedEvent.SetDenial();

            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCreatedEvent.ToApprovalEntity()
            );

            if (approvedEntity != null)
            {
                var accountApprovedEvent = approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountCreatedEvent);
                await _publishEndpoint.Publish(accountApprovedEvent);

                return accountApprovedEvent;
            }

            return null;
        }

        public async Task<IAccountModel?> EvaluateAccountRunningAsync(IAccountModel accountCreatedEvent)
        {
            if (
                string.IsNullOrEmpty(accountCreatedEvent.ProfileId) ||
                string.IsNullOrEmpty(accountCreatedEvent.Id)
            )
                return null;

            var isValidState = await _licenseManager.EvaluateStateEntityAsync(accountCreatedEvent);
            if (isValidState) return accountCreatedEvent;

            var approvalRecord = await _approvalsService.GetOneByParameterAsync(
                a => a != null && a.AccountId == accountCreatedEvent.Id.ToGuid()
            );

            if (approvalRecord != null)
            {
                approvalRecord.SetBlocked();
                var approvedEntity = await _approvalsService.UpdateApprovalAsync(
                    approvalRecord
                );

                if (approvedEntity != null)
                {
                    var accountApprovedEvent =
                        approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountCreatedEvent);
                    await _publishEndpoint.Publish(accountApprovedEvent);

                    return accountApprovedEvent;
                }
            }


            return null;
        }
    }
}