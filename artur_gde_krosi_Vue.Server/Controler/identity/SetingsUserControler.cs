using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler.identity
{
    public class SetingsUserControler : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidationChangeService _accountValidationChangeService;

        public SetingsUserControler(IAccountService accountService, IAccountValidationChangeService accountValidationChangeService)
        {
            _accountService = accountService;
            _accountValidationChangeService = accountValidationChangeService;
        }

        [HttpGet("/giveTokenEmeil")]
        public async Task<IActionResult> giveTokenEmeil(string email)
        {
            return Ok(await _accountService.CodeOnEmailAsync(email));
        }
        [HttpGet("/regEmeil")]
        public async Task<IActionResult> regEmeil(string email, string token)
        {
            return Ok(await _accountService.CheckingEmailTokenAsync(email, token));
        }
    }
}
