using System.Threading.Tasks;
using Billings.States.Transactions;
using MassTransit;
using Sdk.Api.Events.Local;

namespace Billings.Handlers
{
    public class BillingsSubscriptionsHandler : IConsumer<TransactionCheckCommand>
    {
        private readonly ITransactionsContext _transactionsContext;

        public BillingsSubscriptionsHandler(
            ITransactionsContext transactionsContext
        )
        {
            _transactionsContext = transactionsContext;
        }

        public BillingsSubscriptionsHandler()
        {
        }

        public async Task Consume(ConsumeContext<TransactionCheckCommand> context)
        {
            _transactionsContext.InitializeState(new TransactionPending(), context.Message);
            _transactionsContext.CheckBlocked();
            _transactionsContext.CheckDenied();
            _transactionsContext.CheckApproved();
            await _transactionsContext.CheckLicense();
        }
    }
}