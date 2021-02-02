using System;
using System.Threading.Tasks;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.StateMachines
{
    public class TransactionsContext : ITransactionsContext
    {
        private readonly IEventStoreManager<ATransactionsState> _eventStoreManager;
        private readonly ILicenseManager<ITransactionModel> _licenseManager;
        private ATransactionsState _state = null!;

        public TransactionsContext(
            IEventStoreManager<ATransactionsState> eventStoreManager,
            ILicenseManager<ITransactionModel> licenseManager
        )
        {
            _eventStoreManager = eventStoreManager;
            _licenseManager = licenseManager;
        }

        // Common
        public string Id { get; set; } = null!;
        public int Version { get; set; }

        // Foreign
        public string AccountId { get; set; } = null!;
        public string ProfileId { get; set; } = null!;

        // Properties
        public float Amount { get; set; }
        public string Info { get; set; } = null!;
        public string Timestamp { get; set; } = null!;

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent
        public int SequentialNumber { get; set; }

        public ITransactionsContext InitializeState(ATransactionsState state, ITransactionModel transactionModel)
        {
            // Common
            Id = transactionModel.Id;
            Version = transactionModel.Version;
            ProfileId = transactionModel.ProfileId;
            // Properties
            Amount = transactionModel.Amount;
            Info = transactionModel.Info;
            // Approvable
            Approved = transactionModel.Approved;
            Pending = transactionModel.Pending;
            Blocked = transactionModel.Blocked;
            // Concurrent
            SequentialNumber = transactionModel.SequentialNumber;
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
            await _state.HandlePreserveStateAndPublishEvent(_eventStoreManager);
        }

        public void TransitionTo(ATransactionsState state)
        {
            _state = state;
            _state.SetTransaction(this);
        }
    }
}