using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Services.EmailService;
using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountMailingTokenService : IAccountMailingTokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly EmailBodyService _emailBodyService;

        public AccountMailingTokenService(UserManager<ApplicationUser> userManager, IEmailService emailService, EmailBodyService emailBodyService)
        {
            _userManager = userManager;
            _emailService = emailService;
            _emailBodyService = emailBodyService;
        }

        public async Task RegEmailTokenOnEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailService.SendEmailAsync(email,
                "Подтверждение E-mail адреса для доступа к Вашему аккаунту",
                _emailBodyService.EmailBodyRestEmail(user.UserName, user.Email, token));
            return;
        }

        public async Task PasswordResetTokenOnEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendEmailAsync(email,
                "Изменение пароля от Вашего аккаунта",
                    _emailBodyService.EmailBodyPasswordReset(user.UserName, user.Email, token));
            return;
        }

        public async Task ChangeEmailTokenOnEmailAsync(string userName, string newEmail)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) throw new ArgumentException("Пользователь не найден");
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            await _emailService.SendEmailAsync(user.Email,
                "АМЕРИКА СОСАТЬ",
                "\r\nHello [name/email address]\r\n\r\nAre you ready to gain access to all of the assets we prepared for clients of [company]?\r\n\r\nFirst, you must complete your registration by clicking on the button below:\r\n\r\n[button]\r\n\r\nThis link will verify your email address, and then you’ll officially be a part of the [customer portal] community.\r\n\r\nSee you there!\r\n\r\nBest regards, the [company] team" + token);
            return;
        }
    }
}
