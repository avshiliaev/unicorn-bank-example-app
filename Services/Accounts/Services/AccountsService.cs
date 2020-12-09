using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Interfaces;

namespace Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
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