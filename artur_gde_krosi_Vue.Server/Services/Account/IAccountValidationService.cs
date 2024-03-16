using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountValidationService
    {
        Task<(bool, string)> PreliminaryCheckEmeil(string email);
        Task<(bool, string)> PreliminaryCheckUsername(string username);
        Task<IdentityResult> PreliminaryCheckPassword(string password);
    }
}
