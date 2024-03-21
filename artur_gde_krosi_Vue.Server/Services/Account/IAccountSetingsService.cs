using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountSetingsService
    {
        Task<(string name, string surname, string patronymic, bool sendingMail, string Email)> GetInfoUser(string Username);
        Task<bool> RegEmailTokenOnEmailAsync(string email);
        Task<bool> RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail);
        Task<bool> PasswordResetTokenOnEmailAsync(string email);
        Task<IdentityResult> PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword);
    }
}