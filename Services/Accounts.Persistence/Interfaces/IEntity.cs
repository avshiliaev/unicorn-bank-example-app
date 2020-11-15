using System;

namespace Accounts.Persistence.Interfaces
{
    public interface IEntity
    {
        DateTime Created { set; get; }
        DateTime Updated { set; get; }
    }
}