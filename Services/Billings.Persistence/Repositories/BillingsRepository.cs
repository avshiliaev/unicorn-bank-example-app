using Billings.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Billings.Persistence.Repositories
{
    public class BillingsRepository : AbstractRepository<BillingsContext, BillingEntity>
    {
        public BillingsRepository(
            ILogger<AbstractRepository<BillingsContext, BillingEntity>> logger,
            BillingsContext context
        ) : base(logger, context)
        {
        }
    }
}