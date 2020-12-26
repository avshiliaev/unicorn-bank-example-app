using System;
using System.Threading.Tasks;
using Approvals.Persistence.Entities;

namespace Approvals.Interfaces
{
    public interface IApprovalsService
    {
        Task<ApprovalEntity?> CreateApprovalAsync(ApprovalEntity approvalEntity);

        Task<ApprovalEntity?> GetApprovalByIdAsync(Guid approvalId);

        Task<ApprovalEntity?> UpdateApprovalAsync(ApprovalEntity approvalEntity);
    }
}