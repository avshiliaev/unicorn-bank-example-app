using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Routing.Constraints;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        [IsValidGuid] public string? Id { get; set; }
        [Range(0, float.MaxValue)] public float Balance { get; set; }
        [Required] [IsValidGuid] public string ProfileId { get; set; }
        public bool Approved { get; set; }
        [Range(0, int.MaxValue)] public int Version { get; set; }
    }
}