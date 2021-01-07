using System.Collections.Generic;
using Sdk.Api.Interfaces;

namespace Sdk.Api.Dto
{
    public class ProfileDto : IProfileModel<TransactionDto>
    {
        // Common Entity
        public string Id { get; set; }
        public int Version { get; set; }

        // Foreign Properties
        public string ProfileId { get; set; }
        public string AccountId { get; set; }

        // Properties
        public float Balance { get; set; }
        public List<TransactionDto> Transactions { get; set; }

        // Approvable
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public bool Blocked { get; set; }

        // Concurrent
        public int LastSequentialNumber { get; set; }
    }
}