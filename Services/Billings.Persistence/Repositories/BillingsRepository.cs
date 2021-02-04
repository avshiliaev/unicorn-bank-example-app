using Billings.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Billings.Persistence.Repositories
{
    public class BillingsRepository : AbstractEventRepository<BillingsContext, TransactionEntity>
    {
        public BillingsRepository(
            ILogger<AbstractEventRepository<BillingsContext, TransactionEntity>> logger,
            BillingsContext context
        ) : base(logger, context)
        {
        }
    }
}