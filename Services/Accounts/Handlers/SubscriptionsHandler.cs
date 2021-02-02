using System.Threading.Tasks;
using Accounts.States;
using MassTransit;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;

namespace Accounts.Handlers
{
    public class AccountsSubscriptionsHandler :
        IConsumer<AccountIsCheckedEvent>,
        IConsumer<TransactionUpdatedEvent>
    {
        private readonly IAccountContext _accountContext;

        public AccountsSubscriptionsHandler(
            IAccountContext accountContext
        )
        {
            _accountContext = accountContext;
        }

        public AccountsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountIsCheckedEvent> context)
        {
            _accountContext.InitializeState(new AccountPending(), context.Message);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckApproved();
            await _accountContext.CheckLicense();
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            // if (!context.Message.IsApproved()) return;
            // var result = await _eventStoreManager.ProcessTransactionUpdatedEventAsync(context.Message);
            // if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
        }
    }
}