using Sdk.Api.Dto;
using Sdk.Api.Interfaces;

namespace Profiles.Interfaces
{
    public interface IProfilesManager
    {
        ProfileDto? AddNewProfile(IAccountModel accountModel);
        ProfileDto? UpdateProfile(IAccountModel accountModel);
        ProfileDto? AddTransactionToProfile(ITransactionModel transactionModel);
        ProfileDto? UpdateTransactionOnProfile(ITransactionModel transactionModel);
    }
}