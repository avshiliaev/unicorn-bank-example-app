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

        public Task<TransactionEntity> CreateRecordAsync(TransactionEntity accountEntity)
        {
            return _transactionsRepository.AddAsync(accountEntity);
        }

        public Task<List<TransactionEntity>> GetManyRecordsLastVersionAsync(
            Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetManyLastVersionAsync(predicate);
        }

        public Task<TransactionEntity> GetOneRecordAsync(Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _transactionsRepository.GetOneAsync(predicate);
        }
    }
}