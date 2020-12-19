using System;
using Approvals.Persistence.Entities;
using Sdk.Api.Interfaces;
using Sdk.Extensions;

namespace Approvals.Mappers
{
    public static class AccountModelMapper
    {
        public static ApprovalEntity ToApprovalEntity(this IAccountModel accountModel, bool approved)
        {
            return new ApprovalEntity
            {
                Id = Guid.NewGuid(),
                AccountId = accountModel.Id.ToGuid(),
                Approved = approved,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Version = accountModel.Version
            };
        }
    }
}