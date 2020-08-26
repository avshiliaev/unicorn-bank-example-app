using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace DeliveryService.Services
{
    public class DeliveryStatusCheckService : DeliveryStatusCheck.DeliveryStatusCheckBase
    {
        private static readonly Dictionary<string, string> OrderedThings = new Dictionary<string, string>
        {
            {"c1", "o1"},
            {"c2", "o2"},
            {"c3", "o3"}
        };

        private readonly ILogger<DeliveryStatusCheckService> _logger;

        public DeliveryStatusCheckService(ILogger<DeliveryStatusCheckService> logger)
        {
            _logger = logger;
        }

        public override Task<StatusResponse> Status(StatusRequest request, ServerCallContext context)
        {
            return Task.FromResult(new StatusResponse
            {
                IsAccepted = IsOrderAccepted(request.CustomerId, request.OrderId)
            });
        }

        private bool IsOrderAccepted(string customerId, string orderId)
        {
            if (OrderedThings.TryGetValue(customerId, out var order)) 
                return orderId == order;

            return false;
        }
    }
}