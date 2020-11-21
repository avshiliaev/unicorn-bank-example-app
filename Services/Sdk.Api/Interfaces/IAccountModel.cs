using System;
using Sdk.Interfaces;

namespace Sdk.Api.Interfaces
{
    public interface IAccountModel : IDataModel
    {
        public Guid? Id { get; set; }
        public float Balance { get; set; }
        public Guid? ProfileId { get; set; }
        public string Status { get; set; }
    }
}