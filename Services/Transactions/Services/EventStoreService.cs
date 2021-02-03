using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public Task<TransactionEntity> AppendState(TransactionEntity accountEntity)
        {
            return _transactionsRepository.AppendState(accountEntity);
        }

        public Task<List<TransactionEntity>> GetManyLastStatesAsync(
            Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetManyLastStatesAsync(predicate);
        }

        public Task<TransactionEntity> GetOneLastStateAsync(Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetOneLastStateAsync(predicate);
        }
    }
}