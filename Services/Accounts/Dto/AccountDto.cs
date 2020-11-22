using System.ComponentModel.DataAnnotations;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        public string Id { get; set; }
        public float Balance { get; set; }
        [Required] 
        [RequiredGuid] 
        public string ProfileId { get; set; }
        public string Status { get; set; }
    }
}