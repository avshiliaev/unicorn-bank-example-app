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
        private readonly IAccountContext _accountContext = null!;
        private readonly ITransactionsContext _transactionsContext = null!;

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
            await _accountContext.PreserveState();
            await _accountContext.PublishEvent();
        }

        public async Task Consume(ConsumeContext<TransactionUpdatedEvent> context)
        {
            _transactionsContext.InitializeState(new TransactionPending(), context.Message);
            _transactionsContext.CheckBlocked();
            _transactionsContext.CheckDenied();
            _transactionsContext.CheckApproved();
            await _transactionsContext.CheckLicense();
            await _transactionsContext.PreserveState();
            await _transactionsContext.PublishEvent();
        }
    }
}