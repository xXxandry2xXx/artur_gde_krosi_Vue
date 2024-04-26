using artur_gde_krosi_Vue.Server.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountSettingsService
    {
        Task<(string name, string surname, string patronymic, bool sendingMail, string Email)> GetInfoUser(string Username);
        Task RegEmailTokenOnEmailAsync(string email);
        Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail);
        Task PasswordResetTokenOnEmailAsync(string email);
        Task VerifyPasswordResetTokenAsync(string email, string tokinToEmail);
        Task PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword);
        Task ChangeEmailTokenOnEmailAsync(string userName, string newEmail);
        Task ChangeEmailAsync(string userName, string newEmail, string tokinToEmail);
        Task EditSettingsUserAsync(UserInfoModel userInfoModel, string userName);
    }
}