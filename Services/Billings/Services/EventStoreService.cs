using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Billings.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Billings.Services
{
    public class EventStoreService : IEventStoreService<BillingEntity>
    {
        private readonly IRepository<BillingEntity> _billingsRepository;

        public EventStoreService(IRepository<BillingEntity> approvalsRepository)
        {
            _billingsRepository = approvalsRepository;
        }
        
        public Task<BillingEntity> TransactionDecorator(
            Func<BillingEntity, Task<BillingEntity>> func, BillingEntity entity
        )
        {
            return _billingsRepository.TransactionDecorator(func, entity);
        }

        public Task<BillingEntity> AppendStateOfEntity(BillingEntity approvalEntity)
        {
            return _billingsRepository.AppendStateOfEntity(approvalEntity);
        }

        public Task<List<BillingEntity>> GetAllEntitiesLastStatesAsync(
            Expression<Func<BillingEntity, bool>> predicate
        )
        {
            return _billingsRepository.GetAllEntitiesLastStatesAsync(predicate);
        }

        public Task<BillingEntity> GetOneEntityLastStateAsync(Expression<Func<BillingEntity, bool>> predicate)
        {
            return _billingsRepository.GetOneEntityLastStateAsync(predicate);
        }
    }
}