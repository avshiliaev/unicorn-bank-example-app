using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Approvals.Interfaces;
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

        public async Task<ApprovalEntity?> CreateApprovalAsync(ApprovalEntity approvalEntity)
        {
            return await _approvalsRepository.AddAsync(approvalEntity)!;
        }

        public Task<List<ApprovalEntity?>> GetManyLastVersionAsync(
            Expression<Func<ApprovalEntity?, bool>> predicate
        )
        {
            return _approvalsRepository.GetManyLastVersionAsync(predicate!)!;
        }

        public Task<ApprovalEntity?> GetOneAsync(Expression<Func<ApprovalEntity?, bool>> predicate)
        {
            return _approvalsRepository.GetOneAsync(predicate!)!;
        }
    }
}