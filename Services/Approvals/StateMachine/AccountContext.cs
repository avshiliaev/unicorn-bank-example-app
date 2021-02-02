using System;
using System.Threading.Tasks;
using Approvals.Abstractions;
using Approvals.Interfaces;
using Sdk.Api.Interfaces;
using Sdk.License.Interfaces;

namespace Approvals.StateMachine
{
    public class AccountContext : IAccountContext
    {
        private readonly IEventStoreManager<AbstractAccountState> _approvalsManager;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private AbstractAccountState _state = null!;

        public AccountContext(
            IEventStoreManager<AbstractAccountState> approvalsManager,
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

        public IAccountContext InitializeState(AbstractAccountState state, IAccountModel accountModel)
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
            await _state.HandlePreserveStateAndPublishEvent(_approvalsManager);
        }

        public void TransitionTo(AbstractAccountState state)
        {
            _state = state;
            _state.SetAccount(this);
        }
    }
}