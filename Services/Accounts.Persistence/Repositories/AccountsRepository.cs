using Accounts.Persistence.Entities;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Accounts.Persistence.Repositories
{
    public class AccountsRepository : AbstractEventRepository<AccountsContext, AccountEntity>
    {
        public AccountsRepository(
            ILogger<AbstractEventRepository<AccountsContext, AccountEntity>> logger,
            AccountsContext context
        ) : base(logger, context)
        {
        }
    }
}