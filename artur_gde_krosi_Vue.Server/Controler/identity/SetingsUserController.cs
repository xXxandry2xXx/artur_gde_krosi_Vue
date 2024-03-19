using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace artur_gde_krosi_Vue.Server.Controler.identity
{
    [Route("api/identity/[controller]/")]
    [ApiController]
    public class SetingsUserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountSetingsService _accountSetingsService;

        public SetingsUserController(IAccountService accountService, IAccountSetingsService accountSetingsService)
        {
            _accountService = accountService;
            _accountSetingsService = accountSetingsService;
        }
        [HttpGet("GenerateTokenOnChangeEmeil")]
        public async Task<IActionResult> generateTokenOnChangeEmeil(string email)
        {
            return Ok(await _accountSetingsService.RegEmailTokenOnEmailAsync(email));
        }
        [HttpGet("RegEmeil")]
        public async Task<IActionResult> regEmeil(string email, string token)
        {
            return Ok(await _accountSetingsService.RegEmailCheckingEmailTokenAsync(email, token));
        }
        [HttpGet("GenerateTokenOnPasswordReset")]
        public async Task<IActionResult> generateTokenOnPasswordReset(string email)
        {
            return Ok(await _accountSetingsService.PasswordResetTokenOnEmailAsync(email));
        }
        [HttpGet("PasswordReset")]
        public async Task<IActionResult> passwordReset(string email,string token,string newPassword)
        {
            return Ok(await _accountSetingsService.PasswordResetCheckingEmailTokenAsync(email, token, newPassword));
        }
    }
}
