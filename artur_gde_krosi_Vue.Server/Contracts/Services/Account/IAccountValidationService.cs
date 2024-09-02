using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Account
{
    public interface IAccountValidationService
    {
        Task PreliminaryCheckEmeil(string email);
        Task PreliminaryCheckUsername(string username);
        Task PreliminaryCheckPassword(string password);
    }
}
