using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace artur_gde_krosi_Vue.Server.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User"); // Назначаем роль "User"
                                                                 // Другие действия при успешном создании пользователя
            }
            else
            {
                // Обработка ошибок при создании пользователя
            }
            return result;
        }

        public async Task<SignInResult> LoginAsync(string username, string password, bool rememberMe)
        {
            var user = await _userManager.FindByNameAsync(username) ?? await _userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, lockoutOnFailure: false);
                await _userManager.AddToRoleAsync(user, "User");
                return result;
            }
            return SignInResult.Failed;
        }

        public async Task<string> GenerateToken(string username, string email, string password)
        {
            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
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
