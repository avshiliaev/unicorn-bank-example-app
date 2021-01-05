using System.Collections.Generic;
using MongoDB.Driver;
using Profiles.Persistence.Entities;

namespace Profiles.Interfaces
{
    public interface IProfilesService
    {
        List<ProfileEntity> GetAll(string profileId);
        ProfileEntity Get(string id);
        ProfileEntity Create(ProfileEntity entity);
        IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChanges(string profileId);
    }
}