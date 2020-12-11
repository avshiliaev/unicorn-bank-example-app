using System.Net.Mime;
using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsManager _accountsManager;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            ILogger<AccountsController> logger,
            IAccountsManager accountsManager
        )
        {
            _logger = logger;
            _accountsManager = accountsManager;
        }

        [HttpPost("")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> CreateNewAccount(
            [FromBody] AccountDto accountDto
        )
        {
            if (!ModelState.IsValid) return BadRequest();
            var newAccount = await _accountsManager.CreateNewAccountAsync(accountDto);
            return CreatedAtAction(nameof(CreateNewAccount), new {id = newAccount.Id}, newAccount);
        }
    }
}