using System.Threading;
using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Mappers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;

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

        public async Task<IAccountModel?> EvaluateAccountAsync(IAccountModel accountCreatedEvent)
        {
            
            if (
                string.IsNullOrEmpty(accountCreatedEvent.ProfileId) ||  
                string.IsNullOrEmpty(accountCreatedEvent.Id)
            )
                return null;
            
            var approval = true;
            
            var approvedEntity = await _approvalsService.CreateApprovalAsync(
                accountCreatedEvent.ToApprovalEntity(approval)
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