using Accounts.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Interfaces;

namespace Accounts.Mappers
{
    public static class AccountEntityMapper
    {
        public static T ToAccountModel<T>(this AccountEntity accountModel)
            where T : class, IAccountModel, new()
        {
            return new T
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId,
                Approved = accountModel.Approved,
                Id = accountModel.Id.ToString(),
                Version = accountModel.Version
            };
        }

        public static T ToAccountEvent<T>(this AccountEntity accountModel)
            where T : class, IAccountModel, IEvent, new()
        {
            return new T
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId,
                Approved = accountModel.Approved,
                Id = accountModel.Id.ToString(),
                Version = accountModel.Version
            };
        }
    }
}