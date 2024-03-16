using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler.identity
{
    public class SetingsUserControler : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountSetingsService _accountSetingsService;

        public SetingsUserControler(IAccountService accountService, IAccountSetingsService accountSetingsService)
        {
            _accountService = accountService;
            _accountSetingsService = accountSetingsService;
        }

        [HttpGet("/generateTokenOnChangeEmeil")]
        public async Task<IActionResult> generateTokenOnChangeEmeil(string email)
        {
            return Ok(await _accountSetingsService.RegEmailTokenOnEmailAsync(email));
        }
        [HttpGet("/regEmeil")]
        public async Task<IActionResult> regEmeil(string email, string token)
        {
            return Ok(await _accountSetingsService.RegEmailCheckingEmailTokenAsync(email, token));
        }
        [HttpGet("/generateTokenOnPasswordReset")]
        public async Task<IActionResult> generateTokenOnPasswordReset(string email)
        {
            return Ok(await _accountSetingsService.PasswordResetTokenOnEmailAsync(email));
        }
        [HttpGet("/passwordReset")]
        public async Task<IActionResult> passwordReset(string email)
        {
            return Ok(await _accountSetingsService.PasswordResetTokenOnEmailAsync(email));
        }
    }
}
