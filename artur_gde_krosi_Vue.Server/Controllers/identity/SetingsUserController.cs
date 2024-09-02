using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;

namespace artur_gde_krosi_Vue.Server.Controller.identity
{
    [Route("api/identity/[controller]/")]
    [ApiController]
    public class SetingsUserController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountSettingsService _accountSettingsService;
        private readonly IAccountMailingTokenService _accountMailingTokenService;

        public SetingsUserController(IAccountService accountService, IAccountSettingsService accountSetingsService, IAccountMailingTokenService accountMailingTokenService)
        {
            _accountService = accountService;
            _accountSettingsService = accountSetingsService;
            _accountMailingTokenService = accountMailingTokenService;
        }
        
        [HttpGet("GenerateTokenOnRegEmail")]
        public async Task<IActionResult> generateTokenOnChangeEmeil(string email)
        {
            await _accountMailingTokenService.RegEmailTokenOnEmailAsync(email);
            return Ok();
        }
        [HttpPut("RegEmeil")]
        public async Task<IActionResult> regEmeil(string email, string token)
        {
            await _accountSettingsService.RegEmailCheckingEmailTokenAsync(email, token);
            return Ok();
        }
        [HttpPut("RegEmeilOnCode")]
        public async Task<IActionResult> regEmeilOnCode(string email, string code)
        {
            await _accountSettingsService.RegEmailOnCodeAsync(email,code);
            return Ok();
        }
         
        [HttpGet("GenerateTokenOnPasswordReset")]
        public async Task<IActionResult> generateTokenOnPasswordReset(string email)
        {
            await _accountMailingTokenService.PasswordResetTokenOnEmailAsync(email);
            return Ok();
        }
        [HttpPut("VerifyPasswordResetTokenAsync")]
        public async Task<IActionResult> VerifyPasswordResetTokenAsync(string email, string token)
        {
            await _accountSettingsService.VerifyPasswordResetTokenAsync(email, token);
            return Ok();
        }
        [HttpPut("PasswordReset")]
        public async Task<IActionResult> passwordReset(string email, string token, string newPassword)
        {
            await _accountSettingsService.PasswordResetCheckingEmailTokenAsync(email, token, newPassword);
            return Ok();
        }

        [HttpGet("GenerateTokenOnEmailChange")]
        public async Task<IActionResult> generateTokenOnEmailChange(string userName, string newEmail)
        {
            await _accountMailingTokenService.ChangeEmailTokenOnEmailAsync(userName, newEmail);
            return Ok();
        }
        [HttpPut("EmailChange")]
        public async Task<IActionResult> emailChange(string userName, string newEmail, string tokinToEmail)
        {
            await _accountSettingsService.ChangeEmailAsync(userName, newEmail, tokinToEmail);
            return Ok();
        }

        [Authorize(Roles = "User")]
        [HttpGet("UserSettings")]
        public async Task<IActionResult> getInfoUser(string email)
        {
            await _accountSettingsService.GetInfoUser(email);
            return Ok();
        }
        [Authorize(Roles = "User")]
        [HttpPut("UserSettings")]
        public async Task<IActionResult> userSettings([FromForm] UserInfoModel userInfoModel, string userName)
        {
            await _accountSettingsService.EditSettingsUserAsync(userInfoModel, userName);
            return Ok();
        }
    }
}
