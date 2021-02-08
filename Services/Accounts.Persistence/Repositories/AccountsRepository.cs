using Accounts.Persistence.Models;
using Microsoft.Extensions.Logging;
using Sdk.Persistence.Abstractions;

namespace Accounts.Persistence.Repositories
{
    public class AccountsRepository : AbstractEventRepository<AccountsContext, AccountRecord>
    {
        public AccountsRepository(
            ILogger<AbstractEventRepository<AccountsContext, AccountRecord>> logger,
            AccountsContext context
        ) : base(logger, context)
        {
        }
    }
}