using System;

namespace Sdk.Interfaces
{
    public interface IRecord
    {
        public string Id { set; get; }
        public string EntityId { set; get; }
        public string ProfileId { set; get; }
        DateTime Created { set; get; }
        DateTime Updated { set; get; }
        public int Version { get; set; }
    }
}