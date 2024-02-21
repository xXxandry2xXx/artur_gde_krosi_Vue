using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(string username, string email, string password);
        Task<SignInResult> LoginAsync(string username,  string password, bool rememberMe);
        Task<string> GenerateToken(string username, string email, string password);
    }
}