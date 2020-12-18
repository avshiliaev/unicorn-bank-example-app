using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Approvals.Persistence.Repositories
{
    public class ApprovalsRepository : AbstractRepository<ApprovalsContext, ApprovalEntity>
    {
        public ApprovalsRepository(
            ILogger<AbstractRepository<ApprovalsContext, ApprovalEntity>> logger,
            ApprovalsContext context
        ) : base(logger, context)
        {
        }
    }
}