using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Mappers;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Sdk.Integrations;
using UnicornBank.Sdk.ProtoBuffers;

namespace Accounts.Controllers
{
    public class AccountsController : AccountsServiceTemplate
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public override async Task<AccountEvent> Create(AccountEvent accountEvent, ServerCallContext context)
        {
            var createdNewAccount = await _accountsManager.CreateNewAccountAsync(accountEvent.ToNewAccountModel());
            return createdNewAccount.ToAccountEvent();
        }
    }
}