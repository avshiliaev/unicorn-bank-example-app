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
        
        public Task<AccountEntity> CreateRecordAsync(AccountEntity accountEntity)
        {
            return _accountsRepository.AddAsync(accountEntity);
        }

        public Task<List<AccountEntity>> GetManyRecordsLastVersionAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _accountsRepository.GetManyLastVersionAsync(predicate);
        }

        public Task<AccountEntity> GetOneRecordAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _accountsRepository.GetOneAsync(predicate);
        }
    }
}