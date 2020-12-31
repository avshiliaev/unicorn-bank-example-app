using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;
using Transactions.Persistence.Entities;

namespace Transactions.Persistence.Repositories
{
    public class AccountsRepository : AbstractRepository<TransactionsContext, AccountEntity>
    {
        public AccountsRepository(
            ILogger<AbstractRepository<TransactionsContext, AccountEntity>> logger,
            TransactionsContext context
        ) : base(logger, context)
        {
        }
    }
}