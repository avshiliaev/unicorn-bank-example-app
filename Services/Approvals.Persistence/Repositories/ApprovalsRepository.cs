using Approvals.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Approvals.Persistence.Repositories
{
    public class ApprovalsRepository : AbstractEventRepository<ApprovalsContext, AccountEntity>
    {
        public ApprovalsRepository(
            ILogger<AbstractEventRepository<ApprovalsContext, AccountEntity>> logger,
            ApprovalsContext context
        ) : base(logger, context)
        {
        }
    }
}