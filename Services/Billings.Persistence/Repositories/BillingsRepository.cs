using Billings.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Billings.Persistence.Repositories
{
    public class BillingsRepository : AbstractEventRepository<BillingsContext, BillingEntity>
    {
        public BillingsRepository(
            ILogger<AbstractEventRepository<BillingsContext, BillingEntity>> logger,
            BillingsContext context
        ) : base(logger, context)
        {
        }
    }
}