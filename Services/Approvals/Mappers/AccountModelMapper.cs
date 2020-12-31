using System;
using Approvals.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Approvals.Mappers
{
    public static class AccountModelMapper
    {
        public static ApprovalEntity ToApprovalEntity(this IAccountModel accountModel)
        {
            return new ApprovalEntity
            {
                Id = Guid.NewGuid(),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = 0,

                AccountId = accountModel.Id.ToGuid(),
                ProfileId = accountModel.ProfileId,

                Approved = accountModel.Approved,
                Pending = accountModel.Pending,
                Blocked = accountModel.Blocked,
            };
        }
    }
}