using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;

namespace Transactions.States.Transactions
{
    public class TransactionBlocked : ATransactionsState
    {
        public override void HandleCheckBlocked()
        {
            // Remain in the current state.
        }

        public override void HandleCheckDenied()
        {
            if (Context.IsDenied())
                Context.TransitionTo(new TransactionDenied());
            // Otherwise stay.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new TransactionApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseManager<ITransactionModel> licenseManager)
        {
            // Handle as blocked.

            // Check once more a blocked account.
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