using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Interfaces;
using Accounts.Persistence.Models;

namespace Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public Task<AccountModel> CreateAccountAsync(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }

        public Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            throw new NotImplementedException();
        }

        public Task<AccountModel> UpdateAccountAsync(AccountModel accountModel)
        {
            throw new NotImplementedException();
        }
    }
}