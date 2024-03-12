using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public interface IAccountValidationChangeService
    {
        Task<bool> PreliminaryCheckEmeil(string email);
        Task<bool> PreliminaryCheckUsername(string username);
        Task<IdentityResult> PreliminaryCheckPassword(string password);
    }
}
