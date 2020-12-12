using System;

namespace Sdk.Persistence.Interfaces
{
    public interface IEntity
    {
        public Guid Id { set; get; }
        DateTime Created { set; get; }
        DateTime Updated { set; get; }
        public int Version { get; set; }
    }
}