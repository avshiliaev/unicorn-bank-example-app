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
        public override void HandleCheckBlocked()
        {
            if (Blocked)
                Context.TransitionTo(new AccountBlocked());
            // Otherwise stay.
        }

        public override void HandleCheckPending()
        {
            if (!Pending && Approved)
                Context.TransitionTo(new AccountApproved());
            else if (!Pending && !Approved)
                Context.TransitionTo(new AccountDenied());
            // Otherwise stay.
        }

        public override Task HandleProcessState(
            IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            Console.WriteLine("Handle as pending");
            return Task.CompletedTask;
        }
    }
}