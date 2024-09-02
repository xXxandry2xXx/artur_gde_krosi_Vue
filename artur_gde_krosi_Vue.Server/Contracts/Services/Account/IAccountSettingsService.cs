using artur_gde_krosi_Vue.Server.Models.ContelerViews;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Account
{
    public interface IAccountSettingsService
    {
        Task<UserView> GetInfoUser(string Username);
        Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail);
        Task VerifyPasswordResetTokenAsync(string email, string tokinToEmail);
        Task PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword);
        Task ChangeEmailAsync(string userName, string newEmail, string tokinToEmail);
        Task EditSettingsUserAsync(UserInfoModel userInfoModel, string userName);
        Task RegEmailOnCodeAsync(string email, string kode);
    }
}