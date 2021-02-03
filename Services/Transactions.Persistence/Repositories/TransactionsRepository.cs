using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;
using Transactions.Persistence.Entities;

namespace Transactions.Persistence.Repositories
{
    public class TransactionsRepository : AbstractEventRepository<TransactionsContext, TransactionEntity>
    {
        public TransactionsRepository(
            ILogger<AbstractEventRepository<TransactionsContext, TransactionEntity>> logger,
            TransactionsContext context
        ) : base(logger, context)
        {
        }
    }
}