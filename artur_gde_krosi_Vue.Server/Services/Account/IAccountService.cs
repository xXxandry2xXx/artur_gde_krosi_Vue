using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<(IdentityUser user, SignInResult result, IList<string> role)> LoginAsync(string username, string password);
        Task<IdentityResult> AddRoleAsync(string username, string role);
        Task<IdentityResult> DeleteRoleAsync(string username, string role);
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}