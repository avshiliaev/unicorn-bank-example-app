using System;
using System.Threading.Tasks;
using Approvals.Persistence.Entities;
using AutoMapper;
using Sdk.Interfaces;

namespace Approvals.Managers
{
    public class EventStoreManager : IEventStoreManager<AAccountState>
    {
        private readonly IEventStoreService<AccountEntity> _eventStoreService;
        private readonly IMapper _mapper;

        public EventStoreManager(
            IEventStoreService<AccountEntity> eventStoreService,
            IMapper mapper
        )
        {
            _eventStoreService = eventStoreService;
            _mapper = mapper;
        }

        public async Task SaveStateOptimisticallyAsync(AAccountState dataModel)
        {
            var entity = _mapper.Map<AccountEntity>(dataModel);
            var lastState = await _eventStoreService.AppendStateOfEntity(entity);
        }

        public Task<AAccountState> SaveStateAsync(AAccountState dataModel)
        {
            throw new NotImplementedException();
        }
    }
}