using System;
using System.Threading.Tasks;
using Accounts.Abstractions;
using Accounts.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Accounts.StateMachine
{
    public class AccountContext : IAccountContext
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private AbstractState _state = null!;

        public AccountContext(
            IAccountsManager accountsManager,
            ILicenseManager<IAccountModel> licenseManager
        )
        {
            _accountsManager = accountsManager;
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

        public IAccountContext InitializeState(AbstractState state, IAccountModel accountModel)
        {
            Id = accountModel.Id;
            Version = accountModel.Version;
            LastSequentialNumber = accountModel.LastSequentialNumber;
            Balance = accountModel.Balance;
            ProfileId = accountModel.ProfileId;
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
        
        public Type GetCurrentState()
        {
            return _state.GetType();
        }

        public void CheckBlocked()
        {
            _state.HandleCheckBlocked();
        }

        public void CheckDenied()
        {
            _state.HandleCheckDenied();
        }

        public void CheckApproved()
        {
            _state.HandleCheckApproved();
        }

        public async Task CheckLicense()
        {
            await _state.HandleCheckLicense(_licenseManager);
        }
        
        public async Task PreserveStateAndPublishEvent()
        {
            await _state.HandlePreserveStateAndPublishEvent(_accountsManager);
        }
        
    }
}