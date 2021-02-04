using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Approvals.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Approvals.Services
{
    public class EventStoreService : IEventStoreService<AccountEntity>
    {
        private readonly IRepository<AccountEntity> _approvalsRepository;

        public EventStoreService(IRepository<AccountEntity> approvalsRepository)
        {
            _approvalsRepository = approvalsRepository;
        }
        
        public Task<AccountEntity> TransactionDecorator(
            Func<AccountEntity, Task<AccountEntity>> func, AccountEntity entity
        )
        {
            return _approvalsRepository.TransactionDecorator(func, entity);
        }

        public Task<AccountEntity> AppendStateOfEntity(AccountEntity approvalEntity)
        {
            return _approvalsRepository.AppendStateOfEntity(approvalEntity);
        }

        public Task<List<AccountEntity>> GetAllEntitiesLastStatesAsync(
            Expression<Func<AccountEntity, bool>> predicate
        )
        {
            return _approvalsRepository.GetAllEntitiesLastStatesAsync(predicate);
        }

        public Task<AccountEntity> GetOneEntityLastStateAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _approvalsRepository.GetOneEntityLastStateAsync(predicate);
        }
    }
}