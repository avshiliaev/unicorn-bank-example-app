using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Extensions;
using Transactions.States.Transactions;
using Transactions.ViewModels;

namespace Transactions.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ITransactionsContext _transactionsContext;

        public TransactionsController(
            ITransactionsContext transactionsContext,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper
        )
        {
            _transactionsContext = transactionsContext;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
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

            var newTransactionEvent = _mapper.Map<TransactionDto>(transactionViewModel);
            newTransactionEvent.SetPending();

            _transactionsContext.InitializeState(new TransactionPending(), newTransactionEvent);
            _transactionsContext.CheckBlocked();
            _transactionsContext.CheckDenied();
            _transactionsContext.CheckApproved();
            await _transactionsContext.CheckLicense();
            await _transactionsContext.PreserveState();
            await _transactionsContext.PublishEvent();

            return CreatedAtAction(
                nameof(CreateNewTransaction),
                new {id = newTransactionEvent.Id},
                _mapper.Map<TransactionDto>(_transactionsContext)
            );
        }
    }
}