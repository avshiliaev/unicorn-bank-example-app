using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        public string Id { get; set; }
        public float Balance { get; set; }
        [RequiredGuid] public string ProfileId { get; set; }
        public string Status { get; set; }
    }
}