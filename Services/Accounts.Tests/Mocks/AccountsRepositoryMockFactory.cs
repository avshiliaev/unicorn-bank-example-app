using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Interfaces;
using Accounts.Tests.Interfaces;
using Moq;

namespace Accounts.Tests.Mocks
{
    public class AccountsRepositoryMockFactory : IMockFactory<IAccountsRepository>
    {
        public Mock<IAccountsRepository> GetInstance()
        {
            var accountsRepository = new Mock<IAccountsRepository>();
            accountsRepository
                .Setup(a => a.AddAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) => Task.FromResult(accountEntity));
            accountsRepository
                .Setup(a => a.UpdateAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) => Task.FromResult(accountEntity));
            return accountsRepository;
        }
    }
}