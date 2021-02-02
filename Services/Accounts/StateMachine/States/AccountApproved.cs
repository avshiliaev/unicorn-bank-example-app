using System.Threading.Tasks;
using Accounts.Abstractions;
using Accounts.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Accounts.StateMachine.States
{
    public class AccountApproved : AbstractState
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

        public override async Task HandlePreserveStateAndPublishEvent(IAccountsManager accountsManager)
        {
            await accountsManager.SaveStateAndNotifyAsync(this);
        }
    }
}