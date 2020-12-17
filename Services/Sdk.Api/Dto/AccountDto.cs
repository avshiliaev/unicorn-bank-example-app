using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class AccountDto : IAccountModel
    {
        public string? Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public bool Approved { get; set; }
        public int Version { get; set; }
    }
}