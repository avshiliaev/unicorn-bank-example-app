using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Interfaces;

namespace Accounts.Tests.Mocks
{
    public class MockAccountsRepository : IAccountsRepository
    {
        public Task<List<AccountEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> GetByParameterAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountEntity> AddAsync(AccountEntity entity)
        {
            return entity;
        }

        public Task<AccountEntity> UpdateAsync(AccountEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<AccountEntity> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}