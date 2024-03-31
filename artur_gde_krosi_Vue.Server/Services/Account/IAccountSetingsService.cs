using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountSetingsService
    {
        Task<(string name, string surname, string patronymic, bool sendingMail, string Email)> GetInfoUser(string Username);
        Task RegEmailTokenOnEmailAsync(string email);
        Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail);
        Task PasswordResetTokenOnEmailAsync(string email);
        Task<IdentityResult> PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword);
    }
}