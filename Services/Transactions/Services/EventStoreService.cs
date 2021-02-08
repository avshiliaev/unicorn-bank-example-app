using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Services
{
    public class EventStoreService : IEventStoreService<TransactionEntity>
    {
        private readonly IRepository<TransactionEntity> _transactionsRepository;

        public EventStoreService(IRepository<TransactionEntity> transactionsRepository)
        {
            _transactionsRepository = transactionsRepository;
        }
        
        public Task<TransactionEntity> TransactionDecorator(
            Func<TransactionEntity, Task<TransactionEntity>> func, TransactionEntity entity
        )
        {
            return _transactionsRepository.TransactionDecorator(func, entity);
        }

        public Task<TransactionEntity> AppendStateOfEntity(TransactionEntity accountEntity)
        {
            return _transactionsRepository.AppendStateOfEntity(accountEntity);
        }

        public Task<List<TransactionEntity>> GetAllEntitiesLastStatesAsync(
            Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetAllEntitiesLastStatesAsync(predicate);
        }

        public Task<TransactionEntity> GetOneEntityLastStateAsync(Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetOneEntityLastStateAsync(predicate);
        }
    }
}