using System.ComponentModel.DataAnnotations;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        [RoleBased("Administrator")] public string Id { get; set; }
        [RoleBased("Administrator")] public float Balance { get; set; }
        [Required] [RequiredGuid] public string ProfileId { get; set; }
        [RoleBased("Administrator")] public bool Approved { get; set; }
        [RoleBased("Administrator")] public int Version { get; set; }
    }
}