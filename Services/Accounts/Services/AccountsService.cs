using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Sdk.Persistence.Interfaces;

namespace Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IRepository<AccountEntity> _accountsRepository;

        public AccountsService(IRepository<AccountEntity> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public Task<AccountEntity?> CreateAccountAsync(AccountEntity accountModel)
        {
            return _accountsRepository.AddAsync(accountModel)!;
        }

        public Task<AccountEntity?> GetAccountByIdAsync(Guid accountId)
        {
            return _accountsRepository.GetByIdAsync(accountId)!;
        }

        public Task<AccountEntity?> UpdateAccountAsync(AccountEntity accountModel)
        {
            return _accountsRepository.UpdateActivelyAsync(accountModel)!;
        }
    }
}