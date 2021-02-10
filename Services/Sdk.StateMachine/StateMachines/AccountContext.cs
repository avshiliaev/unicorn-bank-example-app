using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;
using Sdk.StateMachine.Interfaces;

namespace Sdk.StateMachine.StateMachines
{
    public class AccountContext : IAccountContext
    {
        private AAccountState _state = null!;

        // Common
        public string Id { get; set; } = null!;
        public int Version { get; set; }
        public string ProfileId { get; set; } = null!;
        public string EntityId { get; set; } = null!;

        // Concurrent Host
        public int LastSequentialNumber { get; set; }

        // Properties
        public float Balance { get; set; }

        // Approvable 
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        public IAccountContext InitializeState(AAccountState state, IAccountModel accountModel)
        {
            Id = accountModel.Id;
            Version = accountModel.Version;
            LastSequentialNumber = accountModel.LastSequentialNumber;
            Balance = accountModel.Balance;
            ProfileId = accountModel.ProfileId;
            EntityId = accountModel.EntityId;
            Approved = accountModel.Approved;
            Pending = accountModel.Pending;
            Blocked = accountModel.Blocked;
            TransitionTo(state);
            return this;
        }

        public AAccountState GetCurrentState()
        {
            return _state;
        }

        public IAccountContext CheckBlocked()
        {
            _state.HandleCheckBlocked();
            return this;
        }

        public IAccountContext CheckDenied()
        {
            _state.HandleCheckDenied();
            return this;
        }

        public IAccountContext CheckApproved()
        {
            _state.HandleCheckApproved();
            return this;
        }

        public async Task CheckLicense(ILicenseService<AAccountState> licenseService)
        {
            await _state.HandleCheckLicense(licenseService);
        }

        public Task PreserveState(IEventStoreService<AAccountState> eventStoreManager)
        {
            return _state.HandlePreserveState(eventStoreManager);
        }

        public Task PublishEvent(IPublishService<AAccountState> publishEndpoint)
        {
            return _state.HandlePublishEvent(publishEndpoint);
        }

        public void TransitionTo(AAccountState state)
        {
            _state = state;
            _state.SetAccount(this);
        }
    }
}