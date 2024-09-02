using artur_gde_krosi_Vue.Server.Contracts.Services.EmailService;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Services.ControlerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controler
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MailingEmailController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly MailingMailSerivce _mailingMailSerivce;
        public MailingEmailController(IEmailService emailService, UserManager<ApplicationUser> userManager, MailingMailSerivce mailingMailSerivce)
        {
            _emailService = emailService;
            _userManager = userManager;
            _mailingMailSerivce = mailingMailSerivce;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string subject, string body)
        {
            _mailingMailSerivce.Get(subject, body);
            return Ok();
        }
    }
}
