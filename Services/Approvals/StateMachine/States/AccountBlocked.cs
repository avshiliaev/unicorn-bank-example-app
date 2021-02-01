using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.Extensions;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine.States
{
    public class AccountBlocked : AbstractState
    {
        public override void HandleCheckBlocked()
        {
            // Remain in the current state.
        }

        public override void HandleCheckDenied()
        {
            if (Context.IsDenied())
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override void HandleCheckApproved()
        {
            if (Context.IsApproved())
                Context.TransitionTo(new AccountApproved());
            // Remain in the current state.
        }

        public override async Task HandleCheckLicense(IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as blocked.

            // Check once more a blocked account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }
    }
}