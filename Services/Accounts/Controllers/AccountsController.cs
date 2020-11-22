using System.Threading.Tasks;
using Accounts.Dto;
using Accounts.Interfaces;
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
        public async Task<ActionResult<AccountDto>> CreateNewAccount(
            [FromBody] AccountDto accountEvent
        )
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _accountsManager.CreateNewAccountAsync(accountEvent));
        }
    }
}