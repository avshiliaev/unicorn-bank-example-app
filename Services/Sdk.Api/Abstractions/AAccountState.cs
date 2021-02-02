using System.Threading.Tasks;
using Sdk.Api.Interfaces;
using Sdk.Api.StateMachines;
using Sdk.Interfaces;

namespace Sdk.Api.Abstractions
{
    public abstract class AAccountState : IAccountModel
    {
        protected AccountContext Context = null!;

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

        public void SetAccount(AccountContext context)
        {
            Context = context;

            Id = context.Id;
            Version = context.Version;
            LastSequentialNumber = context.LastSequentialNumber;
            Balance = context.Balance;
            ProfileId = context.ProfileId;
            Approved = context.Approved;
            Pending = context.Pending;
            Blocked = context.Blocked;
        }

        public abstract void HandleCheckBlocked();
        public abstract void HandleCheckDenied();
        public abstract void HandleCheckApproved();

        public abstract Task HandleCheckLicense(ILicenseManager<IAccountModel> licenseManager);

        public abstract Task HandlePreserveStateAndPublishEvent(
            IEventStoreManager<AAccountState> eventStoreManager
        );
    }
}