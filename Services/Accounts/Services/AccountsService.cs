using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Sdk.Interfaces;

namespace Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IRepository<AccountEntity> _accountsRepository;

        public AccountsService(IRepository<AccountEntity> accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<AccountEntity> CreateAccountAsync(AccountEntity accountModel)
        {
            return await _accountsRepository.AddAsync(accountModel);
        }

        public async Task<AccountEntity> GetAccountByIdAsync(Guid accountId)
        {
            return await _accountsRepository.GetByIdAsync(accountId);
        }

        public async Task<AccountEntity> UpdateAccountAsync(AccountEntity accountModel)
        {
            return await _accountsRepository.UpdateAsync(accountModel);
        }
    }
}