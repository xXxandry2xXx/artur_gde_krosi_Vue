using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Role")]
        public async Task<IActionResult> addRole(string username,string role)
        {
            return Ok(await _accountService.AddRoleAsync(username, role));
        }
        [HttpDelete("Role")]
        public async Task<IActionResult> deleteRole(string username, string role)
        {
            return Ok(await _accountService.DeleteRoleAsync(username, role));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            var result = await _accountService.RegisterAsync(model.Username, model.Email, model.Password);
            if (result)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> login(string usernameOrEmail, string password)   
        {
            var result = await _accountService.LoginAsync(usernameOrEmail, password);
            if (result.result.Succeeded)
            {
                var Token = _accountService.GenerateTokenAsync(result.user); 
                return Ok(new { Token, result.user });
            }
            else
            {
                return Ok(result.result);
            }
        }

    }
}
