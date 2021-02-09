using System.Threading.Tasks;
using Accounts.Persistence.Models;
using AutoMapper;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;
using Sdk.StateMachine.Abstractions;

namespace Accounts.Services
{
    public class EventStoreService : IEventStoreService<AAccountState>
    {
        private readonly IRepository<AccountRecord> _accountsRepository;
        private readonly IMapper _mapper;

        public EventStoreService(
            IRepository<AccountRecord> accountsRepository,
            IMapper mapper
        )
        {
            _accountsRepository = accountsRepository;
            _mapper = mapper;
        }

        public async Task<AAccountState> AppendStateOfEntity(AAccountState accountState)
        {
            var savedEntity = await _accountsRepository.AppendStateOfEntity(
                _mapper.Map<AccountRecord>(accountState)
            );

            var destType = accountState.GetType();
            var mapped = _mapper.Map(savedEntity, savedEntity.GetType(), typeof(AAccountState));
            return (AAccountState) mapped;
        }
    }
}