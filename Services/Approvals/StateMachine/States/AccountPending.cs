using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine.States
{
    public class AccountPending : AbstractState
    {
        public override void SetAccount(AccountContext context)
        {
            Id = context.Id;
            Version = context.Version;
            LastSequentialNumber = context.LastSequentialNumber;
            Balance = context.Balance;
            ProfileId = context.ProfileId;
            Approved = false;
            Pending = true;
            Blocked = false;
        }

        public override void HandleCheckBlocked()
        {
            if (Blocked)
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckDenied()
        {
            if (!Blocked && !Pending && !Approved)
                Context.TransitionTo(new AccountDenied());
        }

        public override void HandleCheckPending()
        {
            if (!Pending && Approved)
                Context.TransitionTo(new AccountApproved());
            else if (!Blocked && !Pending && !Approved)
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override async Task HandleCheckLicense(
            IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            // Handle as pending.
            var isAllowed = await licenseManager.EvaluatePendingAsync(this);
            if (isAllowed)
                Context.TransitionTo(new AccountApproved());
            else
                Context.TransitionTo(new AccountDenied());
        }
    }
}