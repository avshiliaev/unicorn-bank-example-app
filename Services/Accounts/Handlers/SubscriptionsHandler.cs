using System.Threading.Tasks;
using Accounts.Managers;
using MassTransit;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;

namespace Accounts.Handlers
{
    public class AccountsSubscriptionsHandler :
        IConsumer<AccountIsCheckedEvent>,
        IConsumer<TransactionUpdatedEvent>
    {
        private readonly IStatesManager _statesManager;

        public AccountsSubscriptionsHandler(
            IStatesManager statesManager
        )
        {
            _statesManager = statesManager;
        }

        public AccountsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountIsCheckedEvent> context)
        {
            var accountDto = await _statesManager.ProcessAccountState(context.Message);
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            var transactionDto = await _statesManager.ProcessTransactionState(context.Message);
        }
    }
}