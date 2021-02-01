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
        public override void HandleCheckBlocked()
        {
            // Remain in the current state.
        }

        public override void HandleCheckPending()
        {
            if (Pending)
                Context.TransitionTo(new AccountPending());
        }

        public override Task HandleProcessState(
            IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            Console.WriteLine("Handle as blocked");
            return Task.CompletedTask;
        }
    }
}