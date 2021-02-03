using System;
using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static T ToAccountModel<T>(this IAccountContext accountModel) where T : class, IAccountModel, new()
        {
            return new T
            {
                Id = accountModel.Id,
                Version = accountModel.Version,

                Balance = accountModel.Balance,

                ProfileId = accountModel.ProfileId,
                AccountId = accountModel.AccountId,

                Approved = accountModel.Approved,
                Pending = accountModel.Pending,
                Blocked = accountModel.Blocked,

                LastSequentialNumber = accountModel.LastSequentialNumber
            };
        }

        public static AccountEntity ToAccountEntity(this IAccountModel accountEvent)
        {
            return new AccountEntity
            {
                Id = Guid.NewGuid(),
                Version = accountEvent.Version,

                Balance = accountEvent.Balance,

                ProfileId = accountEvent.ProfileId,
                AccountId = accountEvent.AccountId,

                Approved = accountEvent.Approved,
                Pending = accountEvent.Pending,
                Blocked = accountEvent.Blocked,

                LastSequentialNumber = accountEvent.LastSequentialNumber
            };
        }
    }
}