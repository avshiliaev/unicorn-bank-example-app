using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.StateMachines;

namespace Sdk.StateMachine.Abstractions
{
    public abstract class AAccountState : IAccountModel, IEntityState
    {
        
        // TODO: Leave interface, move abstraction to a project?
        protected AccountContext Context = null!;

        // Common
        public string Id { get; set; } = null!;
        public int Version { get; set; }
        public string EntityId { get; set; } = null!;

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

        public void SetAccount(AccountContext context)
        {
            Context = context;

            Id = context.Id;
            Version = context.Version;
            LastSequentialNumber = context.LastSequentialNumber;
            Balance = context.Balance;
            ProfileId = context.ProfileId;
            EntityId = context.EntityId;
            Approved = context.Approved;
            Pending = context.Pending;
            Blocked = context.Blocked;
        }

        public abstract void HandleCheckBlocked();
        public abstract void HandleCheckDenied();
        public abstract void HandleCheckApproved();

        public abstract Task HandleCheckLicense(ILicenseService<AAccountState> licenseService);

        public abstract Task HandlePreserveState(IEventStoreService<AAccountState> eventStoreService);

        public abstract Task HandlePublishEvent(IPublishService<AAccountState> publishEndpoint);
    }
}