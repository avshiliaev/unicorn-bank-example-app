using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Approvals.Interfaces;
using Approvals.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Approvals.Services
{
    public class ApprovalsService : IApprovalsService
    {
        private readonly IRepository<ApprovalEntity> _approvalsRepository;

        public ApprovalsService(IRepository<ApprovalEntity> approvalsRepository)
        {
            _approvalsRepository = approvalsRepository;
        }

        public Task<IEnumerable<ApprovalEntity?>> GetManyByParameterAsync(
            Expression<Func<ApprovalEntity?, bool>> predicate
        )
        {
            return _approvalsRepository.GetManyByParameterAsync(predicate!)!;
        }

        public Task<ApprovalEntity?> GetOneByParameterAsync(Expression<Func<ApprovalEntity?, bool>> predicate)
        {
            return _approvalsRepository.GetOneByParameterAsync(predicate!)!;
        }

        public async Task<ApprovalEntity?> CreateApprovalAsync(ApprovalEntity approvalEntity)
        {
            return await _approvalsRepository.AddAsync(approvalEntity)!;
        }

        public Task<ApprovalEntity?> GetApprovalByIdAsync(Guid approvalId)
        {
            throw new NotImplementedException();
        }

        public Task<ApprovalEntity?> UpdateApprovalAsync(ApprovalEntity approvalEntity)
        {
            return _approvalsRepository.UpdateActivelyAsync(approvalEntity)!;
        }
    }
}