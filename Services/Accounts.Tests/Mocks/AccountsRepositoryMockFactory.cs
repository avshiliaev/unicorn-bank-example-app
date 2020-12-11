using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Accounts.Persistence.Interfaces;
using Accounts.Tests.Extensions;
using Accounts.Tests.Interfaces;
using Moq;

namespace Accounts.Tests.Mocks
{
    public class AccountsRepositoryMockFactory : IMockFactory<IAccountsRepository>
    {
        private List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 0.ToGuid(),
                Balance = 1,
                ProfileId = 0.ToGuid()
            },
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 0.ToGuid()
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid()
            }
        };

        public Mock<IAccountsRepository> GetInstance()
        {
            var accountsRepository = new Mock<IAccountsRepository>();
            accountsRepository
                .Setup(a => a.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(
                    (Guid accountId) => Task.FromResult(
                        _accountEntities.FirstOrDefault(a => a.Id == accountId)
                    )
                );
            accountsRepository
                .Setup(a => a.AddAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) =>
                {
                    accountEntity.Id = Guid.NewGuid();
                    _accountEntities.Add(accountEntity);
                    return Task.FromResult(
                        _accountEntities.FirstOrDefault(a => a.Id == accountEntity.Id)
                    );
                });
            accountsRepository
                .Setup(a => a.UpdateAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) =>
                {
                    var isAccount = _accountEntities.FirstOrDefault(a => a.Id == accountEntity.Id);
                    if (isAccount != null)
                    {
                        _accountEntities = _accountEntities.Where(a => a.Id != accountEntity.Id).ToList();
                        _accountEntities.Add(accountEntity);
                        return Task.FromResult(
                            _accountEntities.FirstOrDefault(a => a.Id == accountEntity.Id)
                        );
                    }
                    return Task.FromResult<AccountEntity>(null);
                });
            return accountsRepository;
        }
    }
}