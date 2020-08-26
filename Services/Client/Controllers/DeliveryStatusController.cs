using System;
using System.Threading.Tasks;
using Client.Interfaces;
using DeliveryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryStatusController : ControllerBase
    {
        /// <summary>
        ///     Logger
        /// </summary>
        private readonly ILogger _logger;

        private readonly IDeliveryStatusService _deliveryStatus;

        public DeliveryStatusController(ILogger<DeliveryStatusController> logger, IDeliveryStatusService deliveryStatus)
        {
            _logger = logger;
            _deliveryStatus = deliveryStatus;
        }

        /// <summary>
        ///     Description
        /// </summary>
        [HttpGet("")]
        public async Task<ActionResult<StatusResponse>> CheckStatus()
        {
            var statusRequest = new StatusRequest { CustomerId = "c1", OrderId = "o1"};
            var status = await _deliveryStatus.GetStatusAsync(statusRequest);
            return Ok(status);
        }
    }
}