using System.Linq;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Profiles.Interfaces;
using Profiles.Mappers;
using Profiles.Persistence.Entities;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Profiles.Managers
{
    public class ProfilesManager : IProfilesManager
    {
        private readonly IProfilesService _profilesService;
        private ILogger<ProfilesManager> _logger;

        public ProfilesManager(
            ILogger<ProfilesManager> logger,
            IProfilesService profilesService
        )
        {
            _logger = logger;
            _profilesService = profilesService;
        }

        public ProfileDto? AddNewProfile(IAccountModel accountModel)
        {
            if (string.IsNullOrEmpty(accountModel.ProfileId))
                return null;
            var profile = _profilesService.Create(accountModel.ToNewProfileEntity());
            return profile?.ToProfileDto();
        }

        public ProfileDto? UpdateProfile(IAccountModel accountModel)
        {
            if (
                string.IsNullOrEmpty(accountModel.ProfileId) ||
                string.IsNullOrEmpty(accountModel.Id)
            )
                return null;

            var updated = _profilesService.UpdatePassively(accountModel.ToNewProfileEntity());
            if (updated != null || !string.IsNullOrEmpty(updated?.Id))
                return updated.ToProfileDto();
            return null;
        }

        public ProfileDto? AddTransactionToProfile(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId)
            )
                return null;
            
            var updated = _profilesService.AddToSet(
                transactionModel.AccountId, 
                transactionModel.ToTransactionSubEntity()
            );
            if (updated != null || !string.IsNullOrEmpty(updated?.Id))
                return updated.ToProfileDto();
            return null;
        }

        public ProfileDto? UpdateTransactionOnProfile(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId)
            )
                return null;
            
            var updated = _profilesService.UpdateInSet(
                transactionModel.AccountId, 
                transactionModel.ToTransactionSubEntity()
            );
            if (updated != null || !string.IsNullOrEmpty(updated?.Id))
                return updated.ToProfileDto();
            return null;
        }
    }
}