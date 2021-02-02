using System.Threading.Tasks;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.Interfaces;

namespace Transactions.States.Account
{
    public class AccountApproved : AAccountState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            if (Context.IsDenied())
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override void HandleCheckApproved()
        {
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as approved.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (!isAllowed)
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override async Task HandlePreserveStateAndPublishEvent(
            IEventStoreManager<IAccountModel> eventStoreManager)
        {
            await eventStoreManager.SaveStateAndNotifyAsync(this);
        }
    }
}