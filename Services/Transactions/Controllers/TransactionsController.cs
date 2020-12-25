using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;
using Transactions.Interfaces;

namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionsManager _transactionsManager;

        public TransactionsController(
            ILogger<TransactionsController> logger,
            ITransactionsManager transactionsManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _logger = logger;
            _transactionsManager = transactionsManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("")]
        [Authorize("write:transactions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> CreateNewTransaction()
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            if (profileId == null) return NotFound();

            var newTransaction = await _transactionsManager.CreateNewTransactionAsync(profileId);
            if (newTransaction == null) return NotFound();

            return CreatedAtAction(nameof(CreateNewTransaction), new {id = newTransaction.Id}, newTransaction);
        }
    }
}