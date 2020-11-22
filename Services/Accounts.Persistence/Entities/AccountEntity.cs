using System;
using Accounts.Persistence.Interfaces;

namespace Accounts.Persistence.Entities
{
    public class AccountEntity : IEntity
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }
        public Guid ProfileId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}