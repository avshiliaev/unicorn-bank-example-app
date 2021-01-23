using System;
using System.Threading.Tasks;
using Approvals.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler : IConsumer<AccountCheckCommand>
    {
        private readonly IApprovalsManager _approvalsManager;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private readonly ILogger<ApprovalsSubscriptionsHandler> _logger;

        public ApprovalsSubscriptionsHandler(
            ILogger<ApprovalsSubscriptionsHandler> logger,
            IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            _logger = logger;
            _approvalsManager = approvalsManager;
            _licenseManager = licenseManager;
        }

        public ApprovalsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountCheckCommand> context)
        {
            _logger.LogDebug($"Received new AccountCheckCommand for {context.Message.Id}");
            IAccountModel? result;

            if (context.Message.IsBlocked()) return;

            if (context.Message.IsPending())
            {
                var isCreationAllowed = await _licenseManager.EvaluateNewEntityAsync(context.Message);
                if (isCreationAllowed)
                    result = await _approvalsManager.ApproveAccountAsync(context.Message);
                else
                    result = await _approvalsManager.DenyAccountAsync(context.Message);
            }
            else
            {
                var isValidState = await _licenseManager.EvaluateStateEntityAsync(context.Message);
                if (isValidState) return;
                result = await _approvalsManager.BlockAccountAsync(context.Message);
            }

            if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}