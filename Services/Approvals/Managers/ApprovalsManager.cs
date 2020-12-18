using System.Threading.Tasks;
using Approvals.Interfaces;
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

        public Task<IAccountModel> EvaluateAccountAsync(IAccountModel accountCreatedEvent)
        {
            var approvedEvent = new AccountApprovedEvent();
            return Task.FromResult<IAccountModel>(approvedEvent);
        }
    }
}