using Accounts.Persistence.Models;
using UnicornBank.Sdk.ProtoBuffers;

namespace Accounts.Mappers
{
    public static class AccountModelMapper
    {
        public static AccountEvent ToAccountEvent(this AccountModel accountModel)
        {
            return new AccountEvent
            {
                Balance = accountModel.Balance,
                Profile = accountModel.ProfileId.ToString(),
                Status = AccountEvent.Types.AccountStatus.TryParse(accountModel.Status),
                Uuid = accountModel.Id.ToString()
            };
        }
    }
}