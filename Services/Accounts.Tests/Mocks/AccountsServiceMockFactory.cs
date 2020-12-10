using System;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Tests.Interfaces;
using Moq;

namespace Accounts.Tests.Mocks
{
    public class AccountsServiceMockFactory : IMockFactory<IAccountsService>
    {
        public Mock<IAccountsService> GetInstance()
        {
            var accountsService = new Mock<IAccountsService>();
            accountsService
                .Setup(a => a.GetAccountByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid accountId) =>
                {
                    var account = new AccountEntity
                    {
                        Id = accountId,
                        Balance = 1,
                        ProfileId = Guid.NewGuid()
                    };
                    return Task.FromResult(account);
                });
            accountsService
                .Setup(a => a.CreateAccountAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) => Task.FromResult(accountEntity));
            accountsService
                .Setup(a => a.UpdateAccountAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) => Task.FromResult(accountEntity));
            return accountsService;
        }
    }
}