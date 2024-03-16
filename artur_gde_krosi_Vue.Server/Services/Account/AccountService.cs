using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Yandex.Cloud.Serverless.Triggers.V1;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IAccountValidationService _accountValidationChangeService;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailService emailService, IAccountValidationService accountValidationChangeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
            _accountValidationChangeService = accountValidationChangeService;
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
            (bool Succeeded, string) checkUsername = await _accountValidationChangeService.PreliminaryCheckUsername(username);
            if (!checkUsername.Succeeded)
            {
                return false;
            }
            (bool Succeeded, string) checkEmail = await _accountValidationChangeService.PreliminaryCheckEmeil(email);
            if (!checkEmail.Succeeded)
            {
                return false;
            }
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return true;
            } 
            return result.Succeeded;
        }

        public async Task<(IdentityUser user, SignInResult result, IList<string> role)> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail) ?? await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);
                var role = await _userManager.GetRolesAsync(user);
                return (user, result, role);
            }
            return (null, SignInResult.Failed, null);
        }

        public async Task<string> GenerateToken(IdentityUser user)
        {
            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),

                new Claim(ClaimTypes.Role,"User")
            };
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:key").Value));
            SigningCredentials sigincredetiols = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                signingCredentials: sigincredetiols);

            string Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Token;
        }
    }
}
