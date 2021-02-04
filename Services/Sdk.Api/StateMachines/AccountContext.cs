using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.StateMachines
{
    public class AccountContext : IAccountContext
    {
        private readonly IEventStoreManager<AAccountState> _approvalsManager;
        private readonly ILicenseManager<IAccountModel> _licenseManager;
        private readonly IPublishEndpoint _publishEndpoint;
        private AAccountState _state = null!;

        public AccountContext(
            IEventStoreManager<AAccountState> approvalsManager,
            ILicenseManager<IAccountModel> licenseManager,
            IPublishEndpoint publishEndpoint
        )
        {
            _approvalsManager = approvalsManager;
            _licenseManager = licenseManager;
            _publishEndpoint = publishEndpoint;
        }

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

        public Task PreserveState()
        {
            return _state.HandlePreserveState(_approvalsManager);
        }

        public Task PublishEvent()
        {
            return _state.HandlePublishEvent(_publishEndpoint);
        }

        public void TransitionTo(AAccountState state)
        {
            _state = state;
            _state.SetAccount(this);
        }
    }
}