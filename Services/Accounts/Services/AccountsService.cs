using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Interfaces;
using Accounts.Persistence.Models;

namespace Accounts.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<AccountModel> CreateAccountAsync(AccountModel accountModel)
        {
            return await _accountsRepository.AddAsync(accountModel);
        }

        public async Task<List<AccountModel>> ListAccountsAsync()
        {
            return await _accountsRepository.ListAllAsync();
        }

        public async Task<AccountModel> GetAccountByIdAsync(Guid accountId)
        {
            return await _accountsRepository.GetByIdAsync(accountId);
        }

        public async Task<AccountModel> UpdateAccountAsync(AccountModel accountModel)
        {
            return await _accountsRepository.UpdateAsync(accountModel);
        }
    }
}