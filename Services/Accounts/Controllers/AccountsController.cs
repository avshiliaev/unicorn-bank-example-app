using System.Net.Mime;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;
using Sdk.Auth.Extensions;

namespace Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsManager _accountsManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountsManager accountsManager,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
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

            var newAccount = await _accountsManager.CreateNewAccountAsync(profileId);
            if (newAccount == null) return NotFound();

            return CreatedAtAction(nameof(CreateNewAccount), new {id = newAccount.Id}, newAccount);
        }
    }
}