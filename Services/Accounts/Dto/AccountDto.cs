using System;
using System.ComponentModel.DataAnnotations;
using Sdk.Api.Interfaces;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        public Guid? Id { get; set; }
        public float Balance { get; set; }

        [Required] public Guid? ProfileId { get; set; }

        public string Status { get; set; }
    }
}