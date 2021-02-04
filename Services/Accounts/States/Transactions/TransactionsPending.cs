using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;

namespace Accounts.States.Transactions
{
    public class TransactionPending : ATransactionsState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new TransactionBlocked());
            // Otherwise stay.
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
            // Handle as pending.
            var isAllowed = await licenseManager.EvaluatePendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new TransactionApproved());
            else
                Context.TransitionTo(new TransactionDenied());
        }

        public override async Task HandlePreserveState(IEventStoreManager<ATransactionsState> eventStoreManager)
        {
            await eventStoreManager.SaveStateOptimisticallyAsync(this);
        }


        public override Task HandlePublishEvent(IPublishEndpoint publishEndpoint)
        {
            throw new NotImplementedException();
        }
    }
}