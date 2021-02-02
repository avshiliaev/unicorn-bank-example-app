using System.Net.Mime;
using System.Threading.Tasks;
using Accounts.Mappers;
using Accounts.States.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Api.Interfaces;
using Sdk.Auth.Extensions;
using Sdk.Extensions;

namespace Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountContext _accountContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsController(
            IAccountContext accountContext,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _accountContext = accountContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("")]
        [Authorize("write:accounts")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> CreateNewAccount()
        {
            var profileId = _httpContextAccessor.GetUserIdentifier();
            if (profileId == null) return NotFound();

            var newAccountEvent = new AccountCreatedEvent {ProfileId = profileId};
            newAccountEvent.SetPending();

            _accountContext.InitializeState(new AccountPending(), newAccountEvent);
            await _accountContext.CheckLicense();
            await _accountContext.PreserveStateAndPublishEvent();

            return CreatedAtAction(
                nameof(CreateNewAccount),
                new {id = newAccountEvent.Id},
                _accountContext.ToAccountModel<AccountDto>()
            );
        }
    }
}