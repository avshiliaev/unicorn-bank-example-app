using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Accounts.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sdk.Api.Dto;
using Sdk.Api.Events;
using Sdk.Auth.Tools;
using Sdk.Extensions;

namespace Accounts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IStatesManager _statesManager;

        public AccountsController(
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IStatesManager statesManager
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _statesManager = statesManager;
        }

        [HttpGet("")]
        [Authorize("write:accounts")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountDto>> CreateNewAccount()
        {
            var profileId = SdkAuthTools.GetUserIdentifier(_httpContextAccessor);
            if (profileId == null) return NotFound();

            var newAccountEvent = new AccountCreatedEvent
            {
                EntityId = Guid.NewGuid().ToString(),
                ProfileId = profileId
            };
            newAccountEvent.SetPending();

            var accountDto = await _statesManager.ProcessAccountState(newAccountEvent);

            return CreatedAtAction(
                nameof(CreateNewAccount),
                new {id = newAccountEvent.Id},
                accountDto
            );
        }
    }
}