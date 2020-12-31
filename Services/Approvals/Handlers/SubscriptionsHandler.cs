using System;
using System.Threading.Tasks;
using Approvals.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler : IConsumer<AccountCheckCommand>
    {
        private readonly IApprovalsManager _approvalsManager;
        private readonly ILogger<ApprovalsSubscriptionsHandler> _logger;

        public ApprovalsSubscriptionsHandler(
            ILogger<ApprovalsSubscriptionsHandler> logger,
            IApprovalsManager approvalsManager
        )
        {
            _logger = logger;
            _approvalsManager = approvalsManager;
        }

        public ApprovalsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountCheckCommand> context)
        {
            _logger.LogDebug($"Received new AccountCheckCommand for {context.Message.Id}");
            if (context.Message.IsBlocked()) return;

            IAccountModel? result;
            if (!context.Message.IsApproved())
                result = await _approvalsManager.EvaluateAccountPendingAsync(context.Message);
            else
                result = await _approvalsManager.EvaluateAccountRunningAsync(context.Message);

            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}