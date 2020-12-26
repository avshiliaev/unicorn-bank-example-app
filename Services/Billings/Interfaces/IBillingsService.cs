using System;
using System.Threading.Tasks;
using Billings.Persistence.Entities;

namespace Billings.Interfaces
{
    public interface IBillingsService
    {
        Task<BillingEntity> CreateBillingAsync(BillingEntity billingEntity);

        Task<BillingEntity> GetBillingByIdAsync(Guid billingId);

        Task<BillingEntity> UpdateBillingAsync(BillingEntity billingEntity);
    }
}