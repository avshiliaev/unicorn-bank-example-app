using System;

namespace Sdk.Interfaces
{
    public interface IDataModel
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}