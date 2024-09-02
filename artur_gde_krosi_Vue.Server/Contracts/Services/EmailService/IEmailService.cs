namespace artur_gde_krosi_Vue.Server.Contracts.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}