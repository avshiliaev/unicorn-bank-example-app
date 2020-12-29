using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Approvals.Persistence.Entities;

namespace Approvals.Interfaces
{
    public interface IApprovalsService
    {
        Task<IEnumerable<ApprovalEntity?>> GetManyByParameterAsync(Expression<Func<ApprovalEntity?, bool>> predicate);
        Task<ApprovalEntity?> CreateApprovalAsync(ApprovalEntity approvalEntity);

        Task<ApprovalEntity?> GetApprovalByIdAsync(Guid approvalId);

        Task<ApprovalEntity?> UpdateApprovalAsync(ApprovalEntity approvalEntity);
    }
}