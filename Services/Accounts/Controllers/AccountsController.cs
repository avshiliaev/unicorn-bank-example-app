using System.Threading.Tasks;
using Accounts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Api.ViewModels;

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

        [HttpGet("")]
        //[AllowAnonymous]
        public async Task<ActionResult<AccountEventViewModel>> GetAllAccounts()
        {
            return Ok(await _accountsManager.ListAccountsAsync());
        }

        [HttpPost("")]
        //[AllowAnonymous]
        public async Task<ActionResult<AccountEventViewModel>> CreateNewAccount(
            [FromForm] AccountEventViewModel accountEvent
        )
        {
            return Ok(await _accountsManager.CreateNewAccountAsync(accountEvent));
        }
    }
}