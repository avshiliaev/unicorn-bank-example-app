using System;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface ITransactionSubModel : IDataModel, IConcurrent, IApprovable
    {
        public string ProfileId { get; set; }
        public float Amount { get; set; }
        public string Info { get; set; }
        public string Timestamp { get; set; }
        public Guid Id { get; set; }
    }

    public interface IProfileModel : IDataModel, IApprovable, IConcurrentHost
    {
        public Guid Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public ITransactionSubModel[] Transactions { get; set; }
    }
}