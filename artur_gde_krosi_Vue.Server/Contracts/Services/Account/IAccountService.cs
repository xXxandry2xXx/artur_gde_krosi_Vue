using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Account
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterModel registerModel, UserInfoModel userInfoModel);
        Task<ApplicationUser> LoginAsync(string usernameOrEmail, string password);
        Task<IdentityResult> AddRoleAsync(string username, string role);
        Task<IdentityResult> AddAdminAsync(string username, string kode);
        Task<IdentityResult> DeleteRoleAsync(string username, string role);
        Task<string> GenerateTokenAsync(ApplicationUser user, bool rememberUser);
    }
}