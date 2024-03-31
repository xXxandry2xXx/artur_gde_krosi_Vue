using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controler
{
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailingEmailController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public MailingEmailController(IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _emailService = emailService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string subject, string body)
        {
            try
            {
                var users = _userManager.Users.Where(x => x.EmailConfirmed == true);
                foreach (var user in users)
                {
                    try
                    {
                        await _emailService.SendEmailAsync(email: user.Email, subject, body);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }
    }
}
