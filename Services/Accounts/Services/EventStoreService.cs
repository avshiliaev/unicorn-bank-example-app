using System.Threading.Tasks;
using Accounts.Persistence.Models;
using AutoMapper;
using Sdk.Api.Abstractions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

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
            var accountEntity = _mapper.Map<AccountRecord>(accountState);
            await _accountsRepository.AppendStateOfEntity(accountEntity);
            return accountState;
        }
    }
}