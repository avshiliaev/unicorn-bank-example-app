using System;
using Sdk.Api.Interfaces;

namespace Sdk.Api.ViewModels
{
    public class AccountEventViewModel : IMessage
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }
        public Guid ProfileId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}