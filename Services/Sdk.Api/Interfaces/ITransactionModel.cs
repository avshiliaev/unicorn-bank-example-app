using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface ITransactionModel : IDataModel, IConcurrent, IApprovable
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string ProfileId { get; set; }
        public float Amount { get; set; }
        public string Info { get; set; }
        public string Timestamp { get; set; }
    }
}