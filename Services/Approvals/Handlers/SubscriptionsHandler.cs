using System.Threading.Tasks;
using Approvals.Interfaces;
using MassTransit;
using Sdk.Api.Events;

namespace Approvals.Handlers
{
    public class ApprovalsSubscriptionsHandler :
        IConsumer<AccountCreatedEvent>,
        IConsumer<AccountUpdatedEvent>
    {
        private readonly IStatesManager _statesManager = null!;

        public ApprovalsSubscriptionsHandler(
            IStatesManager statesManager
        )
        {
            _statesManager = statesManager;
        }

        public ApprovalsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountCreatedEvent> context)
        {
            var accountDto = await _statesManager.ProcessAccountState(context.Message);
        }

        public async Task Consume(ConsumeContext<AccountUpdatedEvent> context)
        {
            var accountDto = await _statesManager.ProcessAccountState(context.Message);
        }
    }
}