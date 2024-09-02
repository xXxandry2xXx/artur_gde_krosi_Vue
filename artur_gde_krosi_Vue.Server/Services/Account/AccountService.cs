using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ExceptionModel;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using NuGet.Versioning;
using Org.BouncyCastle.Ocsp;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountValidationService _accountValidationChangeService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IAccountValidationService accountValidationChangeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _accountValidationChangeService = accountValidationChangeService;
        }

        public async Task RegisterAsync(RegisterModel registerModel, UserInfoModel userInfoModel)
        {
            await _accountValidationChangeService.PreliminaryCheckUsername(registerModel.Username);
            await _accountValidationChangeService.PreliminaryCheckEmeil(registerModel.Email);
            ApplicationUser user = new ApplicationUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Email,
                name = userInfoModel.name,
                surname = userInfoModel.surname,
                patronymic = userInfoModel.patronymic,
                sendingMail = userInfoModel.sendingMail
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                IdentityResult rez = await _userManager.AddToRoleAsync(user, "User");
                IEnumerable<IdentityError> asd = rez.Errors;
                if (!rez.Succeeded) throw new PasswordException(JsonConvert.SerializeObject(rez.Errors), rez.Errors);
                return;
            }
            throw new PasswordException(JsonConvert.SerializeObject(result.Errors), result.Errors);
        }

        public async Task<ApplicationUser> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _userManager.FindByNameAsync(usernameOrEmail) ?? await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user != null)
            {
                var checkPasswordResult = await _userManager.CheckPasswordAsync(user, password);
                if (!checkPasswordResult)
                {
                    throw new ArgumentException("Неверный логин или пароль");
                }

                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    if (result.IsNotAllowed) throw new AllowedOnMailException("Не подтверждена почта");
                    if (!result.IsLockedOut) throw new ArgumentException("Пользователь заблокирован");
                    throw new ArgumentException(JsonConvert.SerializeObject(result));
                }
                return user;
            }
            throw new ArgumentException("Неверный логин или пароль");
        }
        public async Task<IdentityResult> AddRoleAsync(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                IdentityResult rez = await _userManager.AddToRoleAsync(user, role);
                return rez;
            }
            throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Пользователь не найден." }));
        }
        public async Task<IdentityResult> AddAdminAsync(string username, string kode)
        {
            if (kode == _configuration.GetSection("AddAdminKey").Value)
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(username);
                if (user == null) throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Пользователь не найден." }));
                IdentityResult rez = await _userManager.AddToRoleAsync(user, "Admin");
                return rez;
            }
            else throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Код неправельный." }));
        }
        public async Task<IdentityResult> DeleteRoleAsync(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                IdentityResult rez = await _userManager.AddToRoleAsync(user, role);
                return rez;
            }
            throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Пользователь не найден." }));
        }

        public async Task<string> GenerateTokenAsync(ApplicationUser user, bool rememberUser)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:key").Value));
            SigningCredentials sigincredetiols = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(claims: claims,
                expires: rememberUser ? DateTime.Now.AddDays(1) : DateTime.Now.AddDays(15),
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                signingCredentials: sigincredetiols);

            string Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Token;
        }
    }
}
