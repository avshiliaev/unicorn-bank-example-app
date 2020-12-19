using System.Threading.Tasks;
using Approvals.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler
        : IConsumer<AccountCreatedEvent>
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

        public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
        {
            _logger.LogDebug($"Received new AccountCreatedEvent for {context.Message.Id}");
            await _approvalsManager.EvaluateAccountAsync(context.Message);
        }
    }
}