using System;

namespace Sdk.Persistence.Interfaces
{
    public interface IEntity
    {
        DateTime Created { set; get; }
        DateTime Updated { set; get; }
        public int Version { get; set; }
    }
}