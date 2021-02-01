using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine.States
{
    public class AccountBlocked : AbstractState
    {
        public override void SetAccount(AccountContext context)
        {
            Id = context.Id;
            Version = context.Version;
            LastSequentialNumber = context.LastSequentialNumber;
            Balance = context.Balance;
            ProfileId = context.ProfileId;
            Approved = false;
            Pending = false;
            Blocked = true;
        }

        public override void HandleCheckBlocked()
        {
            // Remain in the current state.
        }

        public override void HandleCheckDenied()
        {
            if (!Blocked && !Pending && !Approved)
                Context.TransitionTo(new AccountDenied());
        }

        public override void HandleCheckPending()
        {
            if (Pending)
                Context.TransitionTo(new AccountPending());
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