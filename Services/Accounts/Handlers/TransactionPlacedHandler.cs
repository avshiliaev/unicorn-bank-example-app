using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Sdk.Interfaces;

namespace Accounts.Handlers
{
    public class TransactionPlacedHandler : IConsumer<IDataModel>
    {
        private IAccountsManager _accountsManager;
        private ILogger<AccountApprovedHandler> _logger;

        public TransactionPlacedHandler(
            ILogger<AccountApprovedHandler> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        public Task Consume(ConsumeContext<IDataModel> context)
        {
            throw new NotImplementedException();
        }
    }
}