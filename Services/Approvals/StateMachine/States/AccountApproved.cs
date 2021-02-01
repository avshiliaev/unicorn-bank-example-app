using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine.States
{
    public class AccountApproved : AbstractState
    {
        public override void SetAccount(AccountContext context)
        {
            Id = context.Id;
            Version = context.Version;
            LastSequentialNumber = context.LastSequentialNumber;
            Balance = context.Balance;
            ProfileId = context.ProfileId;
            Approved = true;
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
            // Handle as approved.
            var isAllowed = await licenseManager.EvaluateNotPendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }
    }
}