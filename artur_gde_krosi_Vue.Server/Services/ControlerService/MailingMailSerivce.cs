using artur_gde_krosi_Vue.Server.Contracts.Services.EmailService;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService
{
    public class MailingMailSerivce
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public MailingMailSerivce(IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }
        public async Task Get(string subject, string body)
        {
            var users = _userManager.Users.Where(x => x.EmailConfirmed == true);
            foreach (var user in users)
            {
                try
                {
                    await _emailService.SendEmailAsync(email: user.Email, subject, body);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return ;
        }
    }
}
