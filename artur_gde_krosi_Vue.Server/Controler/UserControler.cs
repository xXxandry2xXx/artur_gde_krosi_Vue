using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.Account;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler
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
            if (result.Succeeded)
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("/PreliminaryCheckEmeil")]
        public async Task<IActionResult> PreliminaryCheckEmeil(string email)
        {
            return Ok(await _accountValidationChangeService.PreliminaryCheckEmeil(email));
        }
        [HttpGet("/PreliminaryCheckUsernamr")]
        public async Task<IActionResult> PreliminaryCheckUsernamr(string username)
        {
            return Ok(await _accountValidationChangeService.PreliminaryCheckUsername(username));
        }
        [HttpGet("/PreliminaryCheckPassword")]
        public async Task<IActionResult> PreliminaryCheckPassword(string password)
        {
            return Ok(await _accountValidationChangeService.PreliminaryCheckPassword(password));
        }

        [HttpGet("/giveTokenEmeil")]
        public async Task<IActionResult> giveTokenEmeil(string email)
        {
            return Ok(await _accountService.CodeOnEmailAsync(email));
        }
        [HttpGet("/regEmeil")]
        public async Task<IActionResult> regEmeil(string email,string token)
        {
            return Ok( await _accountService.CheckingEmailTokenAsync(email, token));
        }

        [HttpPost("/login")]
        public async Task<IActionResult> login(string usernameOrEmail, string password)
        {
            var result = await _accountService.LoginAsync(usernameOrEmail, password);
            if (result.result.Succeeded)
            {
                var Token = _accountService.GenerateToken(result.user);
                return Ok(new { Token , result.user }); 
            }
            else
            {
                return Ok(result.result);
            }
        }

    }
}
