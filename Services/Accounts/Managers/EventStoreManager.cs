using System;
using System.Threading.Tasks;
using Accounts.Mappers;
using Accounts.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Managers
{
    public class EventStoreManager : IEventStoreManager<AAccountState>
    {
        private readonly IEventStoreService<AccountEntity> _eventStoreService;

        public EventStoreManager(
            IEventStoreService<AccountEntity> eventStoreService
        )
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<AAccountState> SaveStateOptimisticallyAsync(AAccountState dataModel)
        {
            var accountEntity = dataModel.ToAccountEntity();

            var lastState = await _eventStoreService.GetOneLastStateAsync(
                a => a.AccountId == dataModel.AccountId &&
                     a.ProfileId == dataModel.ProfileId
                );
            if (lastState.Version.Equals(dataModel.Version))
            {
                var accountEntitySaved = await _eventStoreService.AppendState(accountEntity);
            }
            return dataModel;
        }

        public Task<AAccountState> SaveStateAsync(AAccountState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}