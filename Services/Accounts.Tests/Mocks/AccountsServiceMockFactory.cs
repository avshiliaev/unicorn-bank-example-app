using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Accounts.Persistence.Entities;
using Accounts.Tests.Interfaces;
using Moq;
using Sdk.Tests.Extensions;

namespace Accounts.Tests.Mocks
{
    public class AccountsServiceMockFactory : IMockFactory<IAccountsService>
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

        public Mock<IAccountsService> GetInstance()
        {
            var accountsService = new Mock<IAccountsService>();
            accountsService
                .Setup(a => a.GetAccountByIdAsync(It.IsAny<Guid>()))
                .Returns(
                    (Guid accountId) => Task.FromResult(
                        _accountEntities.FirstOrDefault(a => a.Id == accountId)
                    )
                );
            accountsService
                .Setup(a => a.CreateAccountAsync(It.IsAny<AccountEntity>()))
                .Returns((AccountEntity accountEntity) =>
                {
                    accountEntity.Id = Guid.NewGuid();
                    _accountEntities.Add(accountEntity);
                    return Task.FromResult(
                        _accountEntities.FirstOrDefault(a => a.Id == accountEntity.Id)
                    );
                });
            accountsService
                .Setup(a => a.UpdateAccountAsync(It.IsAny<AccountEntity>()))
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
            return accountsService;
        }
    }
}