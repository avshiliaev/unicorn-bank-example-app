using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Billings.Persistence.Entities;

namespace Billings.Interfaces
{
    public interface IBillingsService
    {
        Task<IEnumerable<BillingEntity?>> GetManyByParameterAsync(Expression<Func<BillingEntity?, bool>> predicate);

        Task<BillingEntity?> CreateBillingAsync(BillingEntity billingEntity);

        Task<BillingEntity?> GetBillingByIdAsync(Guid billingId);

        Task<BillingEntity?> UpdateBillingAsync(BillingEntity billingEntity);
    }
}