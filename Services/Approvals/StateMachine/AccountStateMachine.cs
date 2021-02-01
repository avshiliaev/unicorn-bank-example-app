using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine
{
    public class AccountContext : IAccountContext
    {
        private readonly IApprovalsManager _approvalsManager;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private AbstractState _state = null!;

        public AccountContext(
            IApprovalsManager approvalsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            _approvalsManager = approvalsManager;
            _licenseManager = licenseManager;
        }

        // Common
        public string Id { get; set; } = null!;
        public int Version { get; set; }

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

        // Properties
        public float Balance { get; set; }

        // Foreign
        public string ProfileId { get; set; } = null!;

        // Approvable 
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        public AccountContext InitializeState(AbstractState state, IAccountModel accountModel)
        {
            Balance = accountModel.Balance;
            Approved = accountModel.Approved;
            Pending = accountModel.Pending;
            Blocked = accountModel.Blocked;
            TransitionTo(state);
            return this;
        }
        
        public void TransitionTo(AbstractState state)
        {
            _state = state;
            _state.SetAccount(this);
        }
        
        public void CheckBlocked()
        {
            _state.HandleCheckBlocked();
        }

        public void CheckPending()
        {
            _state.HandleCheckPending();
        }

        public Task ProcessState()
        {
            return _state.HandleProcessState(
                _approvalsManager,
                _licenseManager
            );
        }
    }
}