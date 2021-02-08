using System;
using System.Data;
using System.Threading.Tasks;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IAccountContext : IAccountModel
    {
        IAccountContext InitializeState(IEntityState state, IAccountModel accountModel);
        Type GetCurrentState();
        IAccountContext CheckBlocked();
        IAccountContext CheckDenied();
        IAccountContext CheckApproved();
        Task CheckLicense<TModel>(ILicenseService<TModel> licenseManager) where TModel : class, IEntityState;
        Task PreserveState<TRecord>(IEventStoreService<TRecord> eventStoreService) where TRecord : class, IEntityState;
        Task PublishEvent(IPublishEndpoint publishEndpoint);
    }
}