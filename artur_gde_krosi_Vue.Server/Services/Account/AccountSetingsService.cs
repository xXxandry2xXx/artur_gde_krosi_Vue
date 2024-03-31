using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Common;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountSetingsService : IAccountSetingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountSetingsService(UserManager<ApplicationUser> userManager, IEmailService emailService  )
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<(string name, string surname, string patronymic, bool sendingMail, string Email)> GetInfoUser(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            var rez = (user.name, user.surname, user.patronymic, user.sendingMail , user.Email);
            return rez;
        }
        public async Task RegEmailTokenOnEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendEmailAsync(email,
                    "твой токен",
                    "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team" + token);
                return ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ;
            }
        }
        public async Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, tokinToEmail);
        }
        public async Task PasswordResetTokenOnEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _emailService.SendEmailAsync(email,
                    "твой токен",
                    "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team Code: " + token);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ;
            }
        }
        public async Task<IdentityResult> PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail , string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Пользователь не найден." });
            }
            var result = await _userManager.ResetPasswordAsync(user, tokinToEmail, newPassword);

            return result;
        }
    }
}
