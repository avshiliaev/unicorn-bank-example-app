using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Sdk.Integrations;
using UnicornBank.Sdk.ProtoBuffers;

namespace Accounts.Controllers
{
    public class AccountsController : AccountsServiceTemplate
    {
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger)
        {
            _logger = logger;
        }


        public override Task<AccountEvent> Create(AccountEvent request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }
    }
}