using System;
using Billings.Persistence.Entities;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Api.Mappers;

namespace Billings.Mappers
{
    public class MappingProfile : BaseMapper<TransactionEntity, ITransactionModel>
    {
        public MappingProfile()
        {
            CreateMap<TransactionEntity, ITransactionModel>();
            CreateMap<ITransactionModel, TransactionEntity>();
        }
    }
}