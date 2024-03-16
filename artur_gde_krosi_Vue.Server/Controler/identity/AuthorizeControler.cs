using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler.identity
{
    [ApiController]
    public class AuthorizeControler : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidationService _accountValidationChangeService;

        public AuthorizeControler(IAccountService accountService, IAccountValidationService accountValidationChangeService)
        {
            _accountService = accountService;
            _accountValidationChangeService = accountValidationChangeService;
        }

        [HttpGet("/addRole")]
        public async Task<IActionResult> addRole(string username,string role)
        {
            return Ok(await _accountService.AddRoleAsync(username, role));
        }
        [HttpGet("/deleteRole")]
        public async Task<IActionResult> deleteRole(string username, string role)
        {
            return Ok(await _accountService.DeleteRoleAsync(username, role));
        }

        [HttpPost("/register")]
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

        [HttpPost("/login")]
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
