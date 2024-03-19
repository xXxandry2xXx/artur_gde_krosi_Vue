using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.Account;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yandex.Cloud.Reference;

namespace artur_gde_krosi_Vue.Server.Controler.identity
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
            (bool Succeeded, string error) rez = await _accountValidationChangeService.PreliminaryCheckEmeil(email);
            if (rez.Succeeded) return Ok();
            else return BadRequest(rez.error);

        }
        [HttpGet("PreliminaryCheckUsernamr")]
        public async Task<IActionResult> PreliminaryCheckUsernamr(string username)
        {
            (bool Succeeded, string error) rez = await _accountValidationChangeService.PreliminaryCheckUsername(username);
            if (!rez.Succeeded)
            {
                return BadRequest(rez.error);
            }
            else return Ok();

        }
        [HttpGet("PreliminaryCheckPassword")]
        public async Task<IActionResult> PreliminaryCheckPassword(string password)
        {
            return Ok(await _accountValidationChangeService.PreliminaryCheckPassword(password));
        }

    }
}
