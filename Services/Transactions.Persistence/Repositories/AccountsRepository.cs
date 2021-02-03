using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;
using Transactions.Persistence.Entities;

namespace Transactions.Persistence.Repositories
{
    public class AccountsRepository : AbstractEventRepository<TransactionsContext, AccountEntity>
    {
        public AccountsRepository(
            ILogger<AbstractEventRepository<TransactionsContext, AccountEntity>> logger,
            TransactionsContext context
        ) : base(logger, context)
        {
        }
    }
}