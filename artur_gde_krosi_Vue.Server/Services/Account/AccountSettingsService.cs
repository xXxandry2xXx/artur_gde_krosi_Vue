using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Contracts.Services.EmailService;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews;
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
        private readonly IConfiguration _configuration;


        public AccountSettingsService(UserManager<ApplicationUser> userManager, IEmailService emailService, EmailBodyService emailBodyService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _emailBodyService = emailBodyService;
            _configuration = configuration;
        }

        public async Task<UserView> GetInfoUser(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);
            var rez = new UserView(user.name, user.surname, user.patronymic, user.sendingMail, user.Email);
            return rez;
        }

        public async Task RegEmailCheckingEmailTokenAsync(string email, string tokinToEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            if (user.EmailConfirmed == true) throw new ArgumentException("У пользователя подтверждена почта");
            var result = await _userManager.ConfirmEmailAsync(user, tokinToEmail);
            if (!result.Succeeded) throw new ArgumentException("Ошибка токена");
        }
        public async Task RegEmailOnCodeAsync(string email, string kode)
        {
            if (kode == _configuration.GetSection("AddAdminKey").Value)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(email);
                if (user == null) throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Пользователь не найден." }));
                if (user.EmailConfirmed == false)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
            else throw new ArgumentException(JsonConvert.SerializeObject(new IdentityError { Description = "Код неправельный." }));
        }

        public async Task VerifyPasswordResetTokenAsync(string email, string tokinToEmail)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var tokenValid = await _userManager.VerifyUserTokenAsync(user,
                    _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", tokinToEmail);
            if (!tokenValid) throw new ArgumentException("Ошибка токена");
        }
        public async Task PasswordResetCheckingEmailTokenAsync(string email, string tokinToEmail, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var result = await _userManager.ResetPasswordAsync(user, tokinToEmail, newPassword);
            if (!result.Succeeded) throw new ArgumentException("Ошибка токена");
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
