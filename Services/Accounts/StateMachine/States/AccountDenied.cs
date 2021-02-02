using System.Threading.Tasks;
using Accounts.Abstractions;
using Accounts.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Accounts.StateMachine.States
{
    public class AccountDenied : AbstractState
    {
        public override void HandleCheckBlocked()
        {
            if (Context.IsBlocked())
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            // Remain in the current state.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new AccountApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as denied.

            // Check once more a denied account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }

        public override async Task HandlePreserveStateAndPublishEvent(IAccountsManager accountsManager)
        {
            await accountsManager.SaveStateAndNotifyAsync(this);
        }
    }
}