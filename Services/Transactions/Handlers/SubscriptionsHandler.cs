using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Events;
using Sdk.Api.Events.Local;
using Sdk.Api.Interfaces;
using Transactions.States.Account;
using Transactions.States.Transactions;

namespace Transactions.Handlers
{
    public class TransactionsSubscriptionsHandler :
        IConsumer<TransactionIsCheckedEvent>,
        IConsumer<AccountUpdatedEvent>
    {
        private readonly IAccountContext _accountContext;
        private readonly ITransactionsContext _transactionsContext;

        public TransactionsSubscriptionsHandler(
            IAccountContext accountContext,
            ITransactionsContext transactionsContext
        )
        {
            _accountContext = accountContext;
            _transactionsContext = transactionsContext;
        }

        public TransactionsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<AccountUpdatedEvent> context)
        {
            _accountContext.InitializeState(new AccountPending(), context.Message);
            _accountContext.CheckBlocked();
            _accountContext.CheckDenied();
            _accountContext.CheckApproved();
            await _accountContext.CheckLicense();
            await _accountContext.PreserveStateAndPublishEvent();
        }

        public async Task Consume(ConsumeContext<TransactionIsCheckedEvent> context)
        {
            _transactionsContext.InitializeState(new TransactionPending(), context.Message);
            _transactionsContext.CheckBlocked();
            _transactionsContext.CheckDenied();
            _transactionsContext.CheckApproved();
            await _transactionsContext.CheckLicense();
            await _transactionsContext.PreserveStateAndPublishEvent();
        }
    }
}