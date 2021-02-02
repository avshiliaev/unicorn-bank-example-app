using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sdk.Persistence.Interfaces;
using Transactions.Interfaces;
using Transactions.Persistence.Entities;

namespace Transactions.Services
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

        public Task<AccountEntity?> GetOneByParameterAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return _accountsRepository.GetOneAsync(predicate)!;
        }

        public Task<AccountEntity?> GetAccountByIdAsync(Guid accountId)
        {
            return _accountsRepository.GetByIdAsync(accountId)!;
        }

        public Task<AccountEntity?> UpdateAccountAsync(AccountEntity accountModel)
        {
            return _accountsRepository.UpdateOptimisticallyAsync(accountModel)!;
        }
    }
}