using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Managers;
using MassTransit;
using Sdk.Api.Events;
using Sdk.Api.Events.Domain;

namespace Accounts.Handlers
{
    public class AccountsSubscriptionsHandler :
        IConsumer<AccountProcessedEvent>,
        IConsumer<TransactionUpdatedEvent>
    {
        private readonly IStatesManager _statesManager = null!;

        public AccountsSubscriptionsHandler(
            IStatesManager statesManager
        )
        {
            _statesManager = statesManager;
        }

        public AccountsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountProcessedEvent> context)
        {
            var accountDto = await _statesManager.ProcessAccountState(context.Message);
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            var transactionDto = await _statesManager.ProcessTransactionState(context.Message);
        }
    }
}