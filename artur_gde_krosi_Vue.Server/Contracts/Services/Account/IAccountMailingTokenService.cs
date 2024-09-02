namespace artur_gde_krosi_Vue.Server.Contracts.Services.Account
{
    public interface IAccountMailingTokenService
    {
        Task RegEmailTokenOnEmailAsync(string email);
        Task PasswordResetTokenOnEmailAsync(string email);
        Task ChangeEmailTokenOnEmailAsync(string userName, string newEmail);
    }
}