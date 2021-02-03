using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Approvals.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Approvals.Services
{
    public class EventStoreService : IEventStoreService<ApprovalEntity>
    {
        private readonly IRepository<ApprovalEntity> _approvalsRepository;

        public EventStoreService(IRepository<ApprovalEntity> approvalsRepository)
        {
            _approvalsRepository = approvalsRepository;
        }
        
        public Task<ApprovalEntity> TransactionDecorator(
            Func<ApprovalEntity, Task<ApprovalEntity>> func, ApprovalEntity entity
        )
        {
            return _approvalsRepository.TransactionDecorator(func, entity);
        }

        public Task<ApprovalEntity> AppendState(ApprovalEntity approvalEntity)
        {
            return _approvalsRepository.AppendState(approvalEntity);
        }

        public Task<List<ApprovalEntity>> GetManyLastStatesAsync(
            Expression<Func<ApprovalEntity, bool>> predicate
        )
        {
            return _approvalsRepository.GetManyLastStatesAsync(predicate);
        }

        public Task<ApprovalEntity> GetOneLastStateAsync(Expression<Func<ApprovalEntity, bool>> predicate)
        {
            return _approvalsRepository.GetOneLastStateAsync(predicate);
        }
    }
}