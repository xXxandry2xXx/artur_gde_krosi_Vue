using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.ControlerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controller
{
    //[Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingСartController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ShoppingСartController> _logger;
        private readonly ApplicationIdentityContext db;
        private readonly ShoppingCartService _shoppingCartService;

        public ShoppingСartController(ILogger<ShoppingСartController> logger, ApplicationIdentityContext context, UserManager<ApplicationUser> userManager, ShoppingCartService shoppingCartService)
        {
            _logger = logger;
            db = context;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingСarts(string name)
        {
            var shoppingСarts = await _shoppingCartService.GetShoppingСarts(name);
            return Ok(shoppingСarts);
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingСarts(string name, [FromHeader] string VariantId)
        {
            await _shoppingCartService.AddShoppingСarts(name, VariantId);
            return Ok();
        }

        [Route("/api/[controller]/AddList")]
        [HttpPost]
        public async Task<IActionResult> AddListShoppingСarts(string name, [FromForm] List<string> VariantId)
        {
            await _shoppingCartService.AddListShoppingСarts(name, VariantId);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditShoppingСarts( string ShoppingСartId, int quantity)
        {
            int rezQuantity = await _shoppingCartService.EditShoppingСarts(ShoppingСartId, quantity);
            return Ok(rezQuantity);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteShoppingСarts(string ShoppingСartId)
        {
            await _shoppingCartService.DeleteShoppingСarts(ShoppingСartId);
            return Ok();
        }
    }
}
