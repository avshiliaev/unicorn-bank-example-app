using System;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Managers
{
    public class EventStoreManager : IEventStoreManager<AAccountState>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private IEventStoreService<AccountEntity> _eventStoreService;

        public EventStoreManager(
            IEventStoreService<AccountEntity> eventStoreService,
            IPublishEndpoint publishEndpoint
        )
        {
            _eventStoreService = eventStoreService;
            _publishEndpoint = publishEndpoint;
        }

        public Task<AAccountState> SaveStateAsync(AAccountState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}