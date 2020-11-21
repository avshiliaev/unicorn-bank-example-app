using System;
using Sdk.Api.Interfaces;
using Sdk.Api.Validators;

namespace Accounts.Dto
{
    public class AccountDto : IAccountModel
    {
        public Guid? Id { get; set; }
        public float Balance { get; set; }
        [RequiredGuid] public Guid? ProfileId { get; set; }
        public string Status { get; set; }
    }
}