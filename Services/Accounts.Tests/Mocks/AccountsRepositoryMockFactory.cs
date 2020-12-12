using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accounts.Persistence.Entities;
using Moq;
using Sdk.Persistence.Interfaces;
using Sdk.Tests.Extensions;
using Sdk.Tests.Interfaces;

namespace Accounts.Tests.Mocks
{
    public class AccountsRepositoryMockFactory : IMockFactory<IRepository<AccountEntity>>
    {
        private List<AccountEntity> _accountEntities = new List<AccountEntity>
        {
            new AccountEntity
            {
                Id = 1.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 2.ToGuid(),
                Balance = 1,
                ProfileId = 1.ToGuid(),
                Version = 0
            },
            new AccountEntity
            {
                Id = 3.ToGuid(),
                Balance = 1,
                ProfileId = 2.ToGuid(),
                Version = 0
            }
        };

        public Mock<IRepository<AccountEntity>> GetInstance()
        {
            var accountsRepository = new Mock<IRepository<AccountEntity>>();
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
                    if (
                        accountEntity == null ||
                        Guid.Empty.Equals(accountEntity.Id) ||
                        _accountEntities
                            .FirstOrDefault(
                                acc => acc.Id.Equals(accountEntity.Id) &&
                                       acc.Version.Equals(accountEntity.Version - 1)
                            ) == null
                    )
                        return Task.FromResult<AccountEntity>(null);

                    _accountEntities = _accountEntities.Where(a => a.Id != accountEntity.Id).ToList();
                    _accountEntities.Add(accountEntity);
                    return Task.FromResult(
                        _accountEntities.FirstOrDefault(a => a.Id == accountEntity.Id)
                    );
                });
            return accountsRepository;
        }
    }
}