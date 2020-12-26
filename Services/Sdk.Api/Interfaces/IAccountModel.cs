using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IAccountModel : IDataModel
    {
        public string? Id { get; set; }
        public float Balance { get; set; }
        public string ProfileId { get; set; }
        public bool Approved { get; set; }
        public bool Pending { get; set; }
        public int LastTransactionNumber { get; set; }
    }
}