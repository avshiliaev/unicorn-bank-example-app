using Accounts.Persistence.Models;
using Sdk.Api.ViewModels;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static AccountEventViewModel ToAccountEvent(this AccountModel accountModel)
        {
            return new AccountEventViewModel
            {
                Balance = accountModel.Balance,
                ProfileId = accountModel.ProfileId,
                Status = accountModel.Status,
                Id = accountModel.Id
            };
        }
    }
}