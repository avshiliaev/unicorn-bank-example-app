using System;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface ITransactionsContext : ITransactionModel
    {
        ITransactionsContext InitializeState(ATransactionsState state, ITransactionModel transactionModel);
        Type GetCurrentState();
        ITransactionsContext CheckBlocked();
        ITransactionsContext CheckDenied();
        ITransactionsContext CheckApproved();
        Task CheckLicense(ILicenseService<ITransactionModel> licenseManager);
        Task PreserveState(IEventStoreManager<ATransactionsState> eventStoreManager);
        Task PublishEvent(IPublishEndpoint publishEndpoint);
    }
}