using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;
using Sdk.Extensions;

namespace Approvals.Managers
{
    public class ApprovalsManager : IApprovalsManager
    {
        private readonly IApprovalsService _approvalsService;
        private readonly IPublishEndpoint _publishEndpoint;

        public ApprovalsManager(
            ILogger<ApprovalsManager> logger,
            IApprovalsService approvalsService,
            IPublishEndpoint publishEndpoint
        )
        {
            _approvalsService = approvalsService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IAccountModel?> ApproveAccountAsync(IAccountModel accountCheckCommand)
        {
            if (!accountCheckCommand.IsValid())
                return null;

            accountCheckCommand.SetApproval();

            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCheckCommand.ToApprovalEntity()
            );

            if (approvedEntity == null)
                return null;

            await _publishEndpoint.Publish(
                approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountCheckCommand)
            );

            return accountCheckCommand;
        }

        public async Task<IAccountModel?> DenyAccountAsync(IAccountModel accountCheckCommand)
        {
            if (!accountCheckCommand.IsValid())
                return null;

            accountCheckCommand.SetApproval();

            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCheckCommand.ToApprovalEntity()
            );

            if (approvedEntity == null)
                return null;

            await _publishEndpoint.Publish(
                approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountCheckCommand)
            );

            return accountCheckCommand;
        }

        public async Task<IAccountModel?> BlockAccountAsync(IAccountModel accountCheckCommand)
        {
            if (!accountCheckCommand.IsValid())
                return null;

            var approvalRecord = await _approvalsService.GetOneByParameterAsync(
                a => a != null && a.AccountId == accountCheckCommand.Id.ToGuid()
            );

            if (approvalRecord == null)
                return null;

            approvalRecord.SetBlocked();
            
            var approvedEntity = await _approvalsService.UpdateApprovalAsync(
                approvalRecord
            );

            if (approvedEntity == null)
                return null;

            var accountIsCheckedEvent = approvedEntity.ToAccountModel<AccountIsCheckedEvent>(
                accountCheckCommand
            );
            await _publishEndpoint.Publish(accountIsCheckedEvent);
            return accountIsCheckedEvent;
        }
    }
}