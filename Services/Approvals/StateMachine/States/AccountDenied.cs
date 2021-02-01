using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine.States
{
    public class AccountDenied : AbstractState
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
            Blocked = false;
        }

        public override void HandleCheckBlocked()
        {
            if (Blocked && !Pending)
                Context.TransitionTo(new AccountBlocked());
        }

        public override void HandleCheckDenied()
        {
            // Remain in the current state.
        }

        public override void HandleCheckPending()
        {
            if (Pending)
                Context.TransitionTo(new AccountPending());
        }

        public override async Task HandleCheckLicense(IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager)
        {
            // Handle as denied.
            
            // Check once more a denied account.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            // Otherwise stay.
        }
    }
}