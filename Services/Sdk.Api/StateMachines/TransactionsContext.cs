using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Sdk.Api.StateMachines
{
    public class TransactionsContext : ITransactionsContext
    {

        private ATransactionsState _state = null!;

        // Common
        public string Id { get; set; } = null!;
        public string EntityId { get; set; } = null!;
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
            EntityId = transactionModel.EntityId;
            // Foreign
            AccountId = transactionModel.AccountId;
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

        public ITransactionsContext CheckBlocked()
        {
            _state.HandleCheckBlocked();
            return this;
        }

        public ITransactionsContext CheckDenied()
        {
            _state.HandleCheckDenied();
            return this;
        }

        public ITransactionsContext CheckApproved()
        {
            _state.HandleCheckApproved();
            return this;
        }

        public async Task CheckLicense(ILicenseService<ITransactionModel> licenseManager)
        {
            await _state.HandleCheckLicense(licenseManager);
        }

        public async Task PreserveState(IEventStoreManager<ATransactionsState> eventStoreManager)
        {
            await _state.HandlePreserveState(eventStoreManager);
        }

        public async Task PublishEvent(IPublishEndpoint publishEndpoint)
        {
            await _state.HandlePublishEvent(publishEndpoint);
        }

        public void TransitionTo(ATransactionsState state)
        {
            _state = state;
            _state.SetTransaction(this);
        }
    }
}