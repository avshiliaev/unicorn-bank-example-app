using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;
using Transactions.Persistence.Entities;

namespace Transactions.Persistence.Repositories
{
    public class TransactionsRepository : AbstractRepository<TransactionsContext, TransactionEntity>
    {
        public TransactionsRepository(
            ILogger<AbstractRepository<TransactionsContext, TransactionEntity>> logger,
            TransactionsContext context
        ) : base(logger, context)
        {
        }
    }
}