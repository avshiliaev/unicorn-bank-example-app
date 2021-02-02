using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Auth.Extensions;
using Sdk.Interfaces;
using Transactions.Mappers;
using Transactions.ViewModels;

namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IEventStoreManager<ITransactionModel> _eventStoreManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionsController(
            IEventStoreManager<ITransactionModel> eventStoreManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _eventStoreManager = eventStoreManager;
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

            var newTransactionRequested = transactionViewModel.ToTransactionModel<TransactionDto>(profileId);

            var newTransactionCreated = await _eventStoreManager.SaveStateAndNotifyAsync(
                newTransactionRequested
            );
            if (newTransactionCreated == null) return NotFound();

            return CreatedAtAction(
                nameof(CreateNewTransaction),
                new {id = newTransactionCreated.Id},
                newTransactionCreated
            );
        }
    }
}