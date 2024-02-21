using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler
{
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly IAccountService _accountService;

        public UserControler( IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            var result = await _accountService.RegisterAsync(model.Username, model.Email, model.Password);
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet("/BEBEBE")]
        public async Task<IActionResult> bebebe()
        {
            return Ok("bebebe");
        }

        [HttpPost("/login")]
        public async Task<IActionResult> login([FromForm] RegisterModel model)
        {
            var result = await _accountService.LoginAsync(model.Username, model.Password, model.RememberMe);
            if (result.Succeeded)
            {
                var Token = _accountService.GenerateToken(model.Username, model.Email, model.Password);
                return Ok(Token);
            }
            else
            {
                return Unauthorized("Invalid login attempt");
            }
        }

    }
}
