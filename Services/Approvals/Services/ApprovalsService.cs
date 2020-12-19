using System;
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

        public async Task<ApprovalEntity> CreateApprovalAsync(ApprovalEntity approvalEntity)
        {
            return await _approvalsRepository.AddAsync(approvalEntity);
        }

        public Task<ApprovalEntity> GetApprovalByIdAsync(Guid approvalId)
        {
            throw new NotImplementedException();
        }

        public Task<ApprovalEntity> UpdateApprovalAsync(ApprovalEntity approvalEntity)
        {
            throw new NotImplementedException();
        }
    }
}