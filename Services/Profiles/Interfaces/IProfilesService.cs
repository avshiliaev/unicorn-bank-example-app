using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using Profiles.Persistence.Entities;

namespace Profiles.Interfaces
{
    public interface IProfilesService
    {
        List<ProfileEntity?> GetAll(string profileId);
        List<ProfileEntity?> GetManyByParameter(Expression<Func<ProfileEntity, bool>> predicate);
        ProfileEntity? GetSingleByParameter(Expression<Func<ProfileEntity, bool>> predicate);
        ProfileEntity? Get(string id);
        ProfileEntity? Create(ProfileEntity entity);
        ProfileEntity? UpdatePassively(string id, ProfileEntity entity);
        ProfileEntity? UpdateIgnoreConcurrency(string id, UpdateDefinition<ProfileEntity> updateDefinition);
        IEnumerator<ChangeStreamDocument<ProfileEntity>> SubscribeToChangesMany(string pipeline);
    }
}