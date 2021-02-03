using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;

namespace Accounts.States.Transactions
{
    public class TransactionDenied : ATransactionsState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new TransactionBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            // Remain in the current state.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new TransactionApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseManager<ITransactionModel> licenseManager)
        {
            // Handle as denied.

            // Check once more a denied account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new TransactionApproved());
            // Otherwise stay.
        }

        public override async Task HandlePreserveState(
            IEventStoreManager<ATransactionsState> eventStoreManager)
        {
            await eventStoreManager.SaveStateAndNotifyAsync(this);
        }

        public override Task HandlePublishEvent(IPublishEndpoint publishEndpoint)
        {
            throw new System.NotImplementedException();
        }
    }
}