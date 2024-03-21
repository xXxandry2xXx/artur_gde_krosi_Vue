using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.UserModel;
using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountEditSetings : IAccountEditSetings
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountEditSetings(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<bool> RegisterAsync(UserInfoModel userInfoModel, string userName)
        {
            try
            {
                ApplicationUser? user = await _userManager.FindByNameAsync(userName);
                user.name = userInfoModel.name;
                user.surname = userInfoModel.surname;
                user.patronymic = userInfoModel.patronymic;
                user.sendingMail = userInfoModel.sendingMail;
                return true;
            }
            catch (Exception ex)
            {    
                throw ex;
            }

        }
    }
}
