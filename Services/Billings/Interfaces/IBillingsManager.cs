using System.Threading.Tasks;
using Sdk.Api.Interfaces;

namespace Billings.Interfaces
{
    public interface IBillingsManager
    {
        Task<ITransactionModel> EvaluateTransactionAsync(ITransactionModel accountCreatedEvent);
    }
}