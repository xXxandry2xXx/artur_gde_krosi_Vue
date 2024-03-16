
using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Pkix;
using System.Text.RegularExpressions;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountValidationService : IAccountValidationService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PasswordValidator<IdentityUser> _passwordValidator;

        public AccountValidationService(UserManager<IdentityUser> userManager)
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
        public async Task<(bool, string)> PreliminaryCheckEmeil(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return (false, "неправильный вид почты");
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == trimmedEmail)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user != null)
                    {
                        return (true, "");
                    }
                    else return (false, "Данная почта уже зарегистрирована");
                }
                return (addr.Address == trimmedEmail, "неправильный вид почты");

            }
            catch
            {
                return (false, "");
            }
        }
        public async Task<(bool, string)> PreliminaryCheckUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            if (username.Length <= 5)
            {
                return (false, "длина имени должна быть больше 5");
            }
            if (username.Length > 15)
            {
                return (false, "длина имени должна быть меньше 16");
            }
            if (!Regex.IsMatch(username, pattern))
            {
                return (false, "В имени должны быть только латинские буквы или арабские цифры");
            }
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return (false, "Пользователь с данным ником уже существует");
            }
            return (true, "");
        }
    }
}
