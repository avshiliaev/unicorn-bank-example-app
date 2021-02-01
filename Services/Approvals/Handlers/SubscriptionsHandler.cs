using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.StateMachine.States;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler : IConsumer<AccountCheckCommand>
    {
        private readonly IAccountContext _accountContext = null!;
        private readonly ILogger<ApprovalsSubscriptionsHandler> _logger = null!;

        public ApprovalsSubscriptionsHandler(
            ILogger<ApprovalsSubscriptionsHandler> logger,
            IAccountContext accountContext
        )
        {
            _logger = logger;
            _accountContext = accountContext;
        }

        public ApprovalsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountCheckCommand> context)
        {
            _logger.LogDebug($"Received new AccountCheckCommand for {context.Message.Id}");

            _accountContext.InitializeState(new AccountPending(), context.Message);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckApproved();
            await _accountContext.CheckLicense();
        }
    }
}