
using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkix;
using System.Text.RegularExpressions;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountValidationChangeService : IAccountValidationChangeService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PasswordValidator<IdentityUser> _passwordValidator;

        public AccountValidationChangeService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _passwordValidator = new PasswordValidator<IdentityUser>();
        }

        public async Task<IdentityResult> PreliminaryCheckPassword(string password)
        {
            var user = new ApplicationUser();
            IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, password);

            return result;
        }
        public async Task<bool> PreliminaryCheckEmeil(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> PreliminaryCheckUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            if (username.Length <= 5 && username.Length > 15)
            {
                return false;
            }
            if (Regex.IsMatch(username, pattern))
            {
                return false;
            }

            return true;
        }
    }
}
