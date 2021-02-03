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

        public async Task<AAccountState> SaveStateAsync(AAccountState dataModel)
        {
            var accountEntity = dataModel.ToAccountEntity();
            var accountEntitySaved = await _eventStoreService.CreateRecordAsync(accountEntity);

            return dataModel;
        }
    }
}