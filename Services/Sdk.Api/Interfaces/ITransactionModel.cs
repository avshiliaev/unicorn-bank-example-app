using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface ITransactionModel : IDataModel
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string ProfileId { get; set; }
        public float Amount { get; set; }
        public string Info { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public string Timestamp { get; set; }
        public int SequentialNumber { get; set; }
    }
}