namespace artur_gde_krosi_Vue.Server.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject, string body);
    }
}