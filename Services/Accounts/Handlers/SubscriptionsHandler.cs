using System.Threading.Tasks;
using Accounts.States.Account;
using Accounts.States.Transactions;
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
        private readonly ITransactionsContext _transactionsContext;

        public AccountsSubscriptionsHandler(
            IAccountContext accountContext,
            ITransactionsContext transactionsContext
        )
        {
            _accountContext = accountContext;
            _transactionsContext = transactionsContext;
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
            await _accountContext.PreserveStateAndPublishEvent();
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            // if (!context.Message.IsApproved()) return;
            // var result = await _eventStoreManager.ProcessTransactionUpdatedEventAsync(context.Message);
            // if (result == null) throw new Exception($"Could not process an event {context.Message.Id}");
            _transactionsContext.InitializeState(new TransactionPending(), context.Message);
            _transactionsContext.CheckBlocked();
            _transactionsContext.CheckDenied();
            _transactionsContext.CheckApproved();
            await _transactionsContext.CheckLicense();
            await _transactionsContext.PreserveStateAndPublishEvent();
        }
    }
}