using System.Collections.Generic;
using System.Threading.Tasks;
using DeliveryService;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Sdk.Factories
{
    public abstract class DeliveryStatusCheckServiceFactory: DeliveryStatusCheck.DeliveryStatusCheckBase
    {
        public abstract override Task<StatusResponse> Status(StatusRequest request, ServerCallContext context);
    }
}