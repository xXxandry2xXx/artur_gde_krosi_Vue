using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Contracts.Services.EmailService;

namespace artur_gde_krosi_Vue.Server.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                string smtpHost = _configuration["SmtpSettings:Host"];
                int smtpPort = _configuration.GetValue<int>("SmtpSettings:Port");
                string smtpUsername = _configuration["SmtpSettings:Username"];
                string smtpPassword = _configuration["SmtpSettings:Password"];
                bool enableSsl = _configuration.GetValue<bool>("SmtpSettings:EnableSsl");

                using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = enableSsl;

                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    try
                    {
                        smtpClient.Send(mailMessage);
                        return;
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
