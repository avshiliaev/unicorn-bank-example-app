using System;
using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Sdk.StateMachine.Interfaces
{
    public interface ITransactionsContext : ITransactionModel
    {
        ITransactionsContext InitializeState(ATransactionsState state, ITransactionModel transactionModel);
        Type GetCurrentState();
        ITransactionsContext CheckBlocked();
        ITransactionsContext CheckDenied();
        ITransactionsContext CheckApproved();
        Task CheckLicense(ILicenseService<ATransactionsState> licenseManager);
        Task PreserveState(IEventStoreService<ATransactionsState> eventStoreService);
        Task PublishEvent(IPublishService<ATransactionsState> publishEndpoint);
    }
}