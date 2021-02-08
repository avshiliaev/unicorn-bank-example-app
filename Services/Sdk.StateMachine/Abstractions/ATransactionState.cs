using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.StateMachines;

namespace Sdk.StateMachine.Abstractions
{
    public abstract class ATransactionsState : ITransactionModel, IEntityState
    {
        protected TransactionsContext Context = null!;

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

        public void SetTransaction(TransactionsContext context)
        {
            Context = context;

            // Common
            Id = context.Id;
            Version = context.Version;
            EntityId = context.EntityId;
            ProfileId = context.ProfileId;
            // Properties
            Amount = context.Amount;
            Info = context.Info;
            // Approvable
            Approved = context.Approved;
            Pending = context.Pending;
            Blocked = context.Blocked;
            // Concurrent
            SequentialNumber = context.SequentialNumber;
        }

        public abstract void HandleCheckBlocked();
        public abstract void HandleCheckDenied();
        public abstract void HandleCheckApproved();

        public abstract Task HandleCheckLicense(ILicenseService<ATransactionsState> licenseService);

        public abstract Task HandlePreserveState(IEventStoreService<ATransactionsState> eventStoreService);
        public abstract Task HandlePublishEvent(IPublishService<ATransactionsState> publishEndpoint);
    }
}