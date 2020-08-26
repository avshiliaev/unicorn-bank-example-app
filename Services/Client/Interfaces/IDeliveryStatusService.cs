using System.Threading.Tasks;
using DeliveryService;
using Grpc.Core;

namespace Client.Interfaces
{
    public interface IDeliveryStatusService
    {
        Task<StatusResponse> GetStatusAsync(StatusRequest request);
    }
}