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


        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");

            } 
            return result;
        }



        public async Task<bool> CodeOnEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var rez = await _emailService.SendEmailAsync(email,
                    "твой токен",
                    "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team" + token);
                return rez;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> CheckingEmailTokenAsync(string email, string tokinToEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, tokinToEmail);
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
