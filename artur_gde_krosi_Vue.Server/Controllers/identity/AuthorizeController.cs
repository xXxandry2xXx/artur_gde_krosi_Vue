using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;

namespace artur_gde_krosi_Vue.Server.Controller.identity
{
    [Route("api/identity/[controller]/")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidationService _accountValidationChangeService;

        public AuthorizeController(IAccountService accountService, IAccountValidationService accountValidationChangeService)
        {
            _accountService = accountService;
            _accountValidationChangeService = accountValidationChangeService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Role")]
        public async Task<IActionResult> addRole(string username, string role)
        {
            var rez = await _accountService.AddRoleAsync(username, role);
            if (rez.Succeeded) return Ok();
            else throw new ArgumentException(JsonConvert.SerializeObject(rez));
        }
        [HttpPost("RoleAdmin")]
        public async Task<IActionResult> addAdmin(string username, string kode)
        {
            var rez = await _accountService.AddAdminAsync(username, kode);
            if (rez.Succeeded) return Ok();
            else throw new ArgumentException(JsonConvert.SerializeObject(rez));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Role")]
        public async Task<IActionResult> deleteRole(string username, string role)
        {
            var rez = await _accountService.DeleteRoleAsync(username, role);
            if (rez.Succeeded) return Ok();
            else throw new ArgumentException(JsonConvert.SerializeObject(rez));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel registerModel, [FromForm] UserInfoModel userInfoModel)
        {
            await _accountService.RegisterAsync(registerModel, userInfoModel);
            return Ok();
        }
        [HttpGet("Login")]
        public async Task<IActionResult> login(string usernameOrEmail, string password, bool rememberUser)
        {
            Models.BdModel.ApplicationUser result = await _accountService.LoginAsync(usernameOrEmail, password);
            Task<string> Token = _accountService.GenerateTokenAsync(result, rememberUser);
            return Ok(new { Token, result });
        }

    }
}
