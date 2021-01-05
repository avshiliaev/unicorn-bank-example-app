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

        public async Task<IAccountModel?> EvaluateAccountPendingAsync(IAccountModel accountCheckCommand)
        {
            if (
                string.IsNullOrEmpty(accountCheckCommand.ProfileId) ||
                string.IsNullOrEmpty(accountCheckCommand.Id)
            )
                return null;

            var isCreationAllowed = await _licenseManager.EvaluateNewEntityAsync(accountCheckCommand);

            if (isCreationAllowed)
                accountCheckCommand.SetApproval();
            else
                accountCheckCommand.SetDenial();

            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCheckCommand.ToApprovalEntity()
            );

            if (approvedEntity != null)
            {
                var accountIsCheckedEvent = approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountCheckCommand);
                await _publishEndpoint.Publish(accountIsCheckedEvent);

                return accountIsCheckedEvent;
            }

            return null;
        }

        public async Task<IAccountModel?> EvaluateAccountRunningAsync(IAccountModel accountCheckCommand)
        {
            if (
                string.IsNullOrEmpty(accountCheckCommand.ProfileId) ||
                string.IsNullOrEmpty(accountCheckCommand.Id)
            )
                return null;

            var isValidState = await _licenseManager.EvaluateStateEntityAsync(accountCheckCommand);
            if (isValidState) return accountCheckCommand;

            var approvalRecord = await _approvalsService.GetOneByParameterAsync(
                a => a != null && a.AccountId == accountCheckCommand.Id.ToGuid()
            );

            if (approvalRecord != null)
            {
                approvalRecord.SetBlocked();
                var approvedEntity = await _approvalsService.UpdateApprovalAsync(
                    approvalRecord
                );

                if (approvedEntity != null)
                {
                    var accountIsCheckedEvent = approvedEntity.ToAccountModel<AccountIsCheckedEvent>(
                        accountCheckCommand
                        );
                    await _publishEndpoint.Publish(accountIsCheckedEvent);

                    return accountIsCheckedEvent;
                }
            }


            return null;
        }
    }
}