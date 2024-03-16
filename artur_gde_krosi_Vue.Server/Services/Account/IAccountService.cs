using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<(IdentityUser user, SignInResult result, IList<string> role)> LoginAsync(string username, string password);
        Task<string> GenerateToken(IdentityUser user);
    }
}