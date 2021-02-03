using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Approvals.Persistence.Repositories
{
    public class ApprovalsRepository : AbstractEventRepository<ApprovalsContext, ApprovalEntity>
    {
        public ApprovalsRepository(
            ILogger<AbstractEventRepository<ApprovalsContext, ApprovalEntity>> logger,
            ApprovalsContext context
        ) : base(logger, context)
        {
        }
    }
}