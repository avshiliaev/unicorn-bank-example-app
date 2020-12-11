using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sdk.Interfaces;

namespace Accounts.Persistence.Repositories
{
    public class AccountsRepository : IRepository<AccountEntity>
    {
        private readonly AccountsContext _context;
        private readonly ILogger<AccountsRepository> _logger;

        public AccountsRepository(ILogger<AccountsRepository> logger, AccountsContext context)
        {
            _logger = logger;
            _context = context;
        }

        // TODO: https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-5.0&tabs=visual-studio#performance-considerations
        public async Task<List<AccountEntity>> ListAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<AccountEntity> GetByIdAsync(Guid id)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(acc => acc.Id == id);
        }

        public async Task<AccountEntity> GetByParameterAsync(Expression<Func<AccountEntity, bool>> predicate)
        {
            return await _context.Accounts.Where(predicate).SingleAsync();
        }

        public async Task<AccountEntity> AddAsync(AccountEntity accountModel)
        {
            accountModel.Created = DateTime.UtcNow;
            accountModel.Updated = accountModel.Created;
            var saved = await _context.Accounts.AddAsync(accountModel);
            await _context.SaveChangesAsync();
            return saved.Entity;
        }

        public async Task<AccountEntity> UpdateAsync(AccountEntity accountModel)
        {
            if (
                accountModel == null ||
                Guid.Empty.Equals(accountModel.Id) ||
                !await _context.Accounts.AnyAsync(acc => acc.Id.Equals(accountModel.Id))
            )
                return null;

            accountModel.Updated = DateTime.UtcNow;
            _context.Entry(accountModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return accountModel;
        }

        public async Task<AccountEntity> DeleteAsync(Guid id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return null;

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return account;
        }
    }
}