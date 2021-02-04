using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Accounts.Services
{
    public class EventStoreService : IEventStoreService<AccountEntity>
    {
        private readonly IRepository<AccountEntity> _accountsRepository;

        public EventStoreService(IRepository<AccountEntity> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public Task<AccountEntity> TransactionDecorator(
            Func<AccountEntity, Task<AccountEntity>> func, AccountEntity entity
        )
        {
            return _accountsRepository.TransactionDecorator(func, entity);
        }

        public Task<AccountEntity> AppendStateOfEntity(AccountEntity accountEntity)
        {
            return _accountsRepository.AppendStateOfEntity(accountEntity);
        }

        public Task<List<AccountEntity>> GetAllEntitiesLastStatesAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _accountsRepository.GetAllEntitiesLastStatesAsync(predicate);
        }

        public Task<AccountEntity> GetOneEntityLastStateAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _accountsRepository.GetOneEntityLastStateAsync(predicate);
        }
    }
}