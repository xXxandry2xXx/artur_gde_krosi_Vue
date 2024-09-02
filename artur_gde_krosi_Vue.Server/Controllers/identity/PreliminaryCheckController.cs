using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yandex.Cloud.Reference;

namespace artur_gde_krosi_Vue.Server.Controller.identity
{
    [Route("api/identity/[controller]/")]
    [ApiController]
    public class PreliminaryCheckController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidationService _accountValidationChangeService;

        public PreliminaryCheckController(IAccountService accountService, IAccountValidationService accountValidationChangeService)
        {
            _accountService = accountService;
            _accountValidationChangeService = accountValidationChangeService;
        }

        [HttpGet("PreliminaryCheckEmeil")]
        public async Task<IActionResult> PreliminaryCheckEmeil(string email)
        {
            await _accountValidationChangeService.PreliminaryCheckEmeil(email);
            return Ok();
        }
        [HttpGet("PreliminaryCheckUsername")]
        public async Task<IActionResult> PreliminaryCheckUsernamr(string username)
        {
            await _accountValidationChangeService.PreliminaryCheckUsername(username);
            return Ok();

        }
        [HttpGet("PreliminaryCheckPassword")]
        public async Task<IActionResult> PreliminaryCheckPassword(string password)
        {
            await _accountValidationChangeService.PreliminaryCheckPassword(password);
            return Ok();
        }

    }
}
