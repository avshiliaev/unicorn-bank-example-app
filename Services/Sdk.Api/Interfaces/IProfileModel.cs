using System;
using System.Collections.Generic;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IProfileModel<TTransaction> : 
        IDataModel, IApprovable, IConcurrentHost 
        where TTransaction: class, ITransactionModel
    {
        public string Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public List<TTransaction> Transactions { get; set; }
    }
}