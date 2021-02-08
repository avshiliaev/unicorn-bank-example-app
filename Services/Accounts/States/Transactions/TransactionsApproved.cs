using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Extensions;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.States.Transactions
{
    public class TransactionApproved : ATransactionsState
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
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseService<ATransactionsState> licenseService)
        {
            // Handle as approved.
            var isAllowed = await licenseService.EvaluateNotPendingAsync(this);
            if (!isAllowed)
                Context.TransitionTo(new TransactionBlocked());
            // Otherwise stay.
        }

        public override async Task HandlePreserveState(IEventStoreService<ATransactionsState> eventStoreService)
        {
            await eventStoreService.AppendStateOfEntity(this);
        }

        public override Task HandlePublishEvent(IPublishService<ATransactionsState> publishEndpoint)
        {
            throw new NotImplementedException();
        }
    }
}