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
            return profile?.ToProfilesModel<ProfileDto>();
        }

        public ProfileDto? UpdateProfile(IAccountModel accountModel)
        {
            if (
                string.IsNullOrEmpty(accountModel.ProfileId) ||
                string.IsNullOrEmpty(accountModel.Id)
            )
                return null;
            var profile = _profilesService.Update(accountModel.Id, accountModel.ToProfileEntity());
            return profile?.ToProfilesModel<ProfileDto>();
        }

        public ProfileDto? AddTransactionToProfile(ITransactionModel transactionModel)
        {
            if (
                string.IsNullOrEmpty(transactionModel.ProfileId) ||
                string.IsNullOrEmpty(transactionModel.AccountId)
            )
                return null;

            var profile = _profilesService.Get(transactionModel.ProfileId);
            if (profile != null)
            {
                profile.Transactions.ToList().Add(transactionModel.ToTransactionSubEntity());
                var updated = _profilesService.Update(profile.Id, profile);
                return updated?.ToProfilesModel<ProfileDto>();
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

            var profile = _profilesService.Get(transactionModel.ProfileId);
            if (profile != null)
            {
                profile.Transactions = profile.Transactions
                    .Where(t => t.Id != transactionModel.Id.ToGuid())
                    .ToList();
                profile.Transactions.Add(transactionModel.ToTransactionSubEntity());
                var updated = _profilesService.Update(profile.Id, profile);
                return updated?.ToProfilesModel<ProfileDto>();
            }

            return null;
        }
    }
}