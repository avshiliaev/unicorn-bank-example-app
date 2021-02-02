using System.Threading.Tasks;
using Approvals.States;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler : IConsumer<AccountCheckCommand>
    {
        private readonly IAccountContext _accountContext = null!;

        public ApprovalsSubscriptionsHandler(
            IAccountContext accountContext
        )
        {
            _accountContext = accountContext;
        }

        public ApprovalsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountCheckCommand> context)
        {
            _accountContext.InitializeState(new AccountPending(), context.Message);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckApproved();
            await _accountContext.CheckLicense();
        }
    }
}