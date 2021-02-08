using System.Collections.Generic;

namespace Sdk.Interfaces
{
    public interface IProfileModel<TTransaction> :
        IDataModel, IApprovable, IConcurrentHost
        where TTransaction : class, ITransactionModel
    {
        public string Id { get; set; }

        public float Balance { get; set; }
        public string AccountId { get; set; }

        public List<TTransaction> Transactions { get; set; }
    }
}