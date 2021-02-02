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

        public Task<ApprovalEntity> CreateRecordAsync(ApprovalEntity approvalEntity)
        {
            return _approvalsRepository.AddAsync(approvalEntity);
        }

        public Task<List<ApprovalEntity>> GetManyRecordsLastVersionAsync(
            Expression<Func<ApprovalEntity, bool>> predicate
        )
        {
            return _approvalsRepository.GetManyLastVersionAsync(predicate);
        }

        public Task<ApprovalEntity> GetOneRecordAsync(Expression<Func<ApprovalEntity, bool>> predicate)
        {
            return _approvalsRepository.GetOneAsync(predicate);
        }
    }
}