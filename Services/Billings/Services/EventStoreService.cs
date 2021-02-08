using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Billings.Persistence.Entities;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Billings.Services
{
    public class EventStoreService : IEventStoreService<TransactionEntity>
    {
        private readonly IRepository<TransactionEntity> _billingsRepository;

        public EventStoreService(IRepository<TransactionEntity> approvalsRepository)
        {
            _billingsRepository = approvalsRepository;
        }
        
        public Task<TransactionEntity> TransactionDecorator(
            Func<TransactionEntity, Task<TransactionEntity>> func, TransactionEntity entity
        )
        {
            return _billingsRepository.TransactionDecorator(func, entity);
        }

        public Task<TransactionEntity> AppendStateOfEntity(TransactionEntity approvalEntity)
        {
            return _billingsRepository.AppendStateOfEntity(approvalEntity);
        }

        public Task<List<TransactionEntity>> GetAllEntitiesLastStatesAsync(
            Expression<Func<TransactionEntity, bool>> predicate
        )
        {
            return _billingsRepository.GetAllEntitiesLastStatesAsync(predicate);
        }

        public Task<TransactionEntity> GetOneEntityLastStateAsync(Expression<Func<TransactionEntity, bool>> predicate)
        {
            return _billingsRepository.GetOneEntityLastStateAsync(predicate);
        }
    }
}