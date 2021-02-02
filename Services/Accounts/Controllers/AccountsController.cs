using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Api.Interfaces;
using Sdk.Auth.Extensions;
using Sdk.Interfaces;
using Sdk.Persistence.Interfaces;

namespace Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IEventStoreManager<IAccountModel> _eventStoreManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountsController(
            IEventStoreManager<IAccountModel> eventStoreManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _eventStoreManager = eventStoreManager;
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

            var newAccountRequested = new AccountDto {ProfileId = profileId};
            var newAccountCreated = await _eventStoreManager.SaveStateAndNotifyAsync(newAccountRequested);
            if (newAccountCreated == null) return NotFound();

            return CreatedAtAction(
                nameof(CreateNewAccount), new {id = newAccountCreated.Id}, newAccountCreated
            );
        }
    }
}