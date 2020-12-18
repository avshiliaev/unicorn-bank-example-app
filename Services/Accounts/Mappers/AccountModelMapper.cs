using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static AccountEntity ToAccountEntity(this IAccountModel accountEvent)
        {
            return new AccountEntity
            {
                Id = accountEvent.Id.ToGuid(),
                Balance = accountEvent.Balance,
                ProfileId = accountEvent.ProfileId.ToGuid(),
                Approved = accountEvent.Approved,
                Version = accountEvent.Version
            };
        }
    }
}