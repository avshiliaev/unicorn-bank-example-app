using Billings.Persistence.Entities;
using Sdk.Api.Mappers;
using Sdk.Interfaces;

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