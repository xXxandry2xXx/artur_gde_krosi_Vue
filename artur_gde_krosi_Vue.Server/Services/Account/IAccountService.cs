using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(string username, string email, string password);
        Task<(IdentityUser user, SignInResult result, IList<string> role)> LoginAsync(string username, string password);
        Task<bool> CodeOnEmailAsync(string email);
        Task<bool> CheckingEmailTokenAsync(string email, string tokinToEmail);
        Task<string> GenerateToken(IdentityUser user);
    }
}