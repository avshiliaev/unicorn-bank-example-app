using System;
using System.Threading.Tasks;
using AutoMapper;
using Billings.Mappers;
using Billings.Persistence.Entities;
using MassTransit;
using Sdk.Api.Abstractions;
using Sdk.Api.Dto;
using Sdk.Api.Events.Local;
using Sdk.Api.Validators;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Billings.Managers
{
    public class EventStoreManager : IEventStoreManager<ATransactionsState>
    {
        private readonly IEventStoreService<TransactionEntity> _eventStoreService;
        private readonly IMapper _mapper;

        public EventStoreManager(
            IEventStoreService<TransactionEntity> eventStoreService,
            IMapper mapper
        )
        {
            _eventStoreService = eventStoreService;
            _mapper = mapper;
        }

        public async Task SaveStateOptimisticallyAsync(ATransactionsState dataModel)
        {
            var entity = _mapper.Map<TransactionEntity>(dataModel);
            var lastState = await _eventStoreService.AppendStateOfEntity(entity);
        }

        public Task<ATransactionsState> SaveStateAsync(ATransactionsState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}