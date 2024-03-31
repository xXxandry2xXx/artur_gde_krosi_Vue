using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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

        public ShoppingСartController(ILogger<ShoppingСartController> logger, ApplicationIdentityContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingСarts(string name)
        {
            string username = name;/*HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            var shoppingСarts = db.ShoppingСarts.Include(x => x.Variant).Select(x => new
            {
                ShoppingСartId = x.ShoppingСartId,
                quantity = x.quantity,
                availability = x.Variant.quantityInStock > x.quantity
            });
            return Ok(shoppingСarts);
        }


        [HttpPost]
        public async Task<IActionResult> AddShoppingСarts(string name, [FromForm] string VariantId)
        {
            string username = name;/*HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            if (!db.ShoppingСarts.Any(x => x.UserId == user.Id && x.VariantId == VariantId))
            {
                db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = VariantId, quantity = 0 });
                db.SaveChanges();
                return Ok();
            }
            else throw new ArgumentException("У пользователя в корзине уже имеется данный товар");
        }

        [Route("/api/[controller]/AddList")]
        [HttpPost]
        public async Task<IActionResult> AddListShoppingСarts(string name, [FromForm] List<string> VariantId)
        {

            string username = name;/* HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            bool error = false;
            foreach (var item in VariantId)
            {
                if (!db.ShoppingСarts.Any(x => x.UserId == user.Id && x.VariantId == item))
                {
                    db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = item });
                }
                else error = true;
            }
            db.SaveChanges();
            if (!error) return Ok();
            else throw new ArgumentException("Один или несколько товаров уже лежали в корзине");
        }

        [HttpPut]
        public async Task<IActionResult> EditShoppingСarts([FromForm] string ShoppingСartId, [FromForm] int quantity)
        {

            if (quantity > 0)
            {
                ShoppingСart? shoppingCart = db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).FirstOrDefault();
                if (shoppingCart != null)
                {
                    shoppingCart.quantity = quantity; 
                    db.SaveChanges();

                    return Ok((quantity, true));
                }
                else throw new ArgumentException("Позиция не найдена.");
            }
            throw new ArgumentException("Количество продукции должно быть положительным и не равным нул.");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteShoppingСarts([FromForm] string ShoppingСartId)
        {
            var rez = await db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).ExecuteDeleteAsync();
            db.SaveChanges();

            return Ok("ок");
        }
    }
}
