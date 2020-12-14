using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Accounts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sdk.Api.Dto;

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

        [HttpPost("{profileId:Guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> CreateNewAccount(Guid profileId)
        {
            if (profileId == Guid.Empty) return NotFound();
            var newAccount = await _accountsManager.CreateNewAccountAsync(profileId);
            return CreatedAtAction(nameof(CreateNewAccount), new {id = newAccount.Id}, newAccount);
        }
    }
}