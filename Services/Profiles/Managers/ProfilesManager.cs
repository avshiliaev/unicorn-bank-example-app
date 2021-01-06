using System.Linq;
using Microsoft.Extensions.Logging;
using Profiles.Interfaces;
using Profiles.Mappers;
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

            var profile = _profilesService.GetSingleByParameter(
                e => e.AccountId == accountModel.Id && e.ProfileId == accountModel.ProfileId
            );
            if (profile != null || !string.IsNullOrEmpty(profile?.Id))
            {
                // Keep Id and Transactions
                var newEntity = accountModel.ToProfileEntity();
                newEntity.Id = profile.Id;
                newEntity.Transactions = profile.Transactions;
                
                var updated = _profilesService.UpdatePassively(newEntity.Id, newEntity);
                return updated?.ToProfileDto();
            }

            return null;
        }

        public ProfileDto? AddTransactionToProfile(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId)
            )
                return null;

            var profile = _profilesService.GetSingleByParameter(
                e => 
                    e.AccountId == transactionModel.AccountId 
                    && e.ProfileId == transactionModel.ProfileId
            );
            if (profile != null || !string.IsNullOrEmpty(profile?.Id))
            {
                // Optimistic Concurrency Control: check last transaction version
                if (!profile.CheckConcurrentController(transactionModel))
                    return null;
                
                profile.Transactions.Add(transactionModel.ToTransactionSubEntity());
                var updated = _profilesService.UpdateIgnoreConcurrency(profile.Id, profile);
                return updated?.ToProfileDto();
            }

            return null;
        }

        public ProfileDto? UpdateTransactionOnProfile(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId)
            )
                return null;

            var profile = _profilesService.GetSingleByParameter(
                e => 
                    e.AccountId == transactionModel.AccountId 
                    && e.ProfileId == transactionModel.ProfileId
            );
            if (profile != null || !string.IsNullOrEmpty(profile?.Id))
            {
                // Optimistic Concurrency Control: check last transaction version
                if (!profile.CheckConcurrentController(transactionModel))
                    return null;
                
                // If there is no such transaction or the transaction version is wrong
                if (
                    !profile.Transactions.Select(
                        t => t.Id == transactionModel.Id 
                             && t.Version.Equals(transactionModel.Version - 1)).Any()
                )
                    return null;
                
                // Update the entire transaction
                profile.Transactions = profile.Transactions
                    .Where(t => t.Id != transactionModel.Id)
                    .ToList();
                profile.Transactions.Add(transactionModel.ToTransactionSubEntity());
                
                var updated = _profilesService.UpdateIgnoreConcurrency(profile.Id, profile);
                return updated?.ToProfileDto();
            }

            return null;
        }
    }
}