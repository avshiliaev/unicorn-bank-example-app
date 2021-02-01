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
        public override void HandleCheckBlocked()
        {
            if (Blocked && !Pending)
                Context.TransitionTo(new AccountBlocked());
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
            Console.WriteLine("Handle as denied");
            return Task.CompletedTask;
        }
    }
}