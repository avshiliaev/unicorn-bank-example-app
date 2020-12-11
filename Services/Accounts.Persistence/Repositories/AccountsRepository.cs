using Accounts.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Accounts.Persistence.Repositories
{
    public class AccountsRepository : AbstractRepository<AccountsContext, AccountEntity>
    {
        public AccountsRepository(
            ILogger<AbstractRepository<AccountsContext, AccountEntity>> logger,
            AccountsContext context
        ) : base(logger, context)
        {
        }
    }
}