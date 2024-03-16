using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler.identity
{
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IAccountValidationChangeService _accountValidationChangeService;

        public UserControler(IAccountService accountService, IAccountValidationChangeService accountValidationChangeService)
        {
            _accountService = accountService;
            _accountValidationChangeService = accountValidationChangeService;
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
                var Token = _accountService.GenerateToken(result.user);
                return Ok(new { Token, result.user });
            }
            else
            {
                return Ok(result.result);
            }
        }

    }
}
