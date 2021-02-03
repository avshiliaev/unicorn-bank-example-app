using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Auth.Extensions;
using Transactions.Mappers;
using Transactions.States.Transactions;
using Transactions.ViewModels;

namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionsContext _transactionsContext;

        public TransactionsController(
            ITransactionsContext transactionsContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _transactionsContext = transactionsContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("")]
        [Authorize("write:transactions")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransactionDto>> CreateNewTransaction(
            [FromQuery] TransactionViewModel transactionViewModel
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            var profileId = _httpContextAccessor.GetUserIdentifier();
            if (profileId == null) return NotFound();

            var newTransactionEvent = transactionViewModel.ToTransactionModel<TransactionCreatedEvent>(profileId);

            _transactionsContext.InitializeState(new TransactionPending(), newTransactionEvent);
            await _transactionsContext.CheckLicense();
            await _transactionsContext.PreserveState();

            return CreatedAtAction(
                nameof(CreateNewTransaction),
                new {id = newTransactionEvent.Id},
                _transactionsContext.ToTransactionModel<TransactionDto>()
            );
        }
    }
}