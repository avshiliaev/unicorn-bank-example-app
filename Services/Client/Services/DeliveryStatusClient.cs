using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryService;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;

namespace DeliveryClient
{
    public class DeliveryStatusClient
    {
        private readonly ILogger<DeliveryStatusClient> _logger;

        public DeliveryStatusClient(ILogger<DeliveryStatusClient> logger)
        {
            _logger = logger;
        }

        public async Task<StatusResponse> GetStatusAsync(StatusRequest request, ServerCallContext context)
        {
            // The port number(5001) must match the port of the gRPC server.
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client =  new DeliveryStatusCheck.DeliveryStatusCheckClient(channel);
            var reply = await client.StatusAsync(request);

            return reply;
        }
    }
}