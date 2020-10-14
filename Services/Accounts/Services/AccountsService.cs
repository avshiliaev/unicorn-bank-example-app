using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Sdk.Integrations;
using UnicornBankSdk;

namespace Accounts
{
    public class AccountsService : AccountsServiceTemplate
    {
        private readonly ILogger<AccountsService> _logger;

        public AccountsService(ILogger<AccountsService> logger)
        {
            _logger = logger;
        }


        public override Task<AccountEvent> Create(AccountEvent request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}