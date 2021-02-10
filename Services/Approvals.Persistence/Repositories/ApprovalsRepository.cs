using Approvals.Persistence.Models;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Approvals.Persistence.Repositories
{
    public class ApprovalsRepository : AbstractEventRepository<ApprovalsContext, AccountRecord>
    {
        public ApprovalsRepository(
            ILogger<AbstractEventRepository<ApprovalsContext, AccountRecord>> logger,
            ApprovalsContext context
        ) : base(logger, context)
        {
        }
    }
}