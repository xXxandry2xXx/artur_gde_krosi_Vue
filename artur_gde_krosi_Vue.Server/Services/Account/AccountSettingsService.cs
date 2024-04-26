using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using artur_gde_krosi_Vue.Server.Services.EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;
using NuGet.Common;
using Yandex.Cloud.Iam.V1;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly EmailBodyService _emailBodyService;

        public AccountSettingsService(UserManager<ApplicationUser> userManager, IEmailService emailService, EmailBodyService emailBodyService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _emailBodyService = emailBodyService;
        }
        public async Task<(string name, string surname, string patronymic, bool sendingMail, string Email)> GetInfoUser(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            var rez = (user.name, user.surname, user.patronymic, user.sendingMail, user.Email);
            return rez;
        }
        public async Task RegEmailTokenOnEmailAsync(string email)
        {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null) throw new ArgumentException("Пользователь не найден");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendEmailAsync(email,
                    "твой токен",
                    _emailBodyService.EmailBodyPasswordReset(user.UserName, user.Email, token));
                return;
        }
        public async Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            if (user.EmailConfirmed == true) throw new ArgumentException("У пользователя подтверждена почта");
            var result = await _userManager.ConfirmEmailAsync(user, tokinToEmail);
            if (!result.Succeeded) throw new ArgumentException("Ошибка токена");
        }
        public async Task PasswordResetTokenOnEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendEmailAsync(email,
                "твой токен",
                "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team Code: " + token);
            return;
        }

        public async Task<IdentityResult> PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var result = await _userManager.ResetPasswordAsync(user, tokinToEmail, newPassword);

            return result;
        }

        public async Task ChangeEmailTokenOnEmailAsync(string userName, string newEmail)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            await _emailService.SendEmailAsync(user.Email,
                "твой токен",
                "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team" + token);
            return;
        }
        public async Task ChangeEmailAsync(string userName, string newEmail, string tokinToEmail)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, tokinToEmail);
        }

        public async Task EditSettingsUserAsync(UserInfoModel userInfoModel, string userName)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            user.name = userInfoModel.name;
            user.surname = userInfoModel.surname;
            user.patronymic = userInfoModel.patronymic;
            user.sendingMail = userInfoModel.sendingMail;
            var rez = await _userManager.UpdateAsync(user);
            if (rez.Succeeded) return;
            else throw new ArgumentException(JsonConvert.SerializeObject(rez));
        }
    }
}
