using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

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

        public async Task<IAccountModel?> CreateOrUpdateStateAsyncAsync(IAccountModel accountModel)
        {
            if (!accountModel.IsValid())
                return null!;

            var approvedEntity = await _approvalsService.UpdateApprovalAsync(
                accountModel.ToApprovalEntity()
            );

            if (approvedEntity == null)
                return null;

            var accountIsCheckedEvent = approvedEntity.ToAccountModel<AccountIsCheckedEvent>(
                approvedEntity.ToAccountModel<AccountIsCheckedEvent>(accountModel)
            );
            await _publishEndpoint.Publish(accountIsCheckedEvent);
            return accountIsCheckedEvent;
        }
    }
}