using artur_gde_krosi_Vue.Server.Contracts.Services.Account;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Pkix;
using System.Text.RegularExpressions;

namespace artur_gde_krosi_Vue.Server.Services.Account
{
    public class AccountValidationService : IAccountValidationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PasswordValidator<ApplicationUser> _passwordValidator;

        public AccountValidationService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _passwordValidator = new PasswordValidator<ApplicationUser>();
        }

        public async Task PreliminaryCheckPassword(string password)
        {
            var user = new ApplicationUser();
            IdentityResult result = await _passwordValidator.ValidateAsync(_userManager, user, password);

            if (result.Succeeded) return;
            else throw new ArgumentException(JsonConvert.SerializeObject(result));
        }
        public async Task PreliminaryCheckEmeil(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                throw new ArgumentException("неправильный вид почты");
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == trimmedEmail)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    if (user == null)return;
                    else throw new ArgumentException("Данная почта уже зарегистрирована");
                }
                throw new ArgumentException("неправильный вид почты");
            }
            catch (ArgumentException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new ArgumentException("неправильный вид почты");
            }
        }
        public async Task PreliminaryCheckUsername(string username)
        {
            string pattern = @"^[a-zA-Z0-9]+$";
            if (username.Length <= 5) throw new ArgumentException("длина имени должна быть больше 5");
            if (username.Length > 15) throw new ArgumentException("длина имени должна быть меньше 16");
            if (!Regex.IsMatch(username, pattern)) throw new ArgumentException("В имени должны быть только латинские буквы или арабские цифры");
            if (await _userManager.FindByNameAsync(username) != null) throw new ArgumentException("Пользователь с таким именем уже существует");
        }
    }
}
