using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controler
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]/")]
    [ApiController]
    public class ShoppingСartController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ShoppingСartController> _logger;
        private readonly ApplicationIdentityContext db;

        public ShoppingСartController(ILogger<ShoppingСartController> logger, ApplicationIdentityContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            db = context;
            _userManager = userManager;
        }

        [Route("Get")]
        [HttpGet]
        public async Task<IActionResult> GetShoppingСarts()
        {
            string username = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            var shoppingСarts = db.ShoppingСarts.Include(x => x.Variant).Select(x => new
            {
                ShoppingСartId = x.ShoppingСartId,
                quantity = x.quantity,
                availability = x.Variant.quantityInStock > x.quantity
            });
            return Ok(shoppingСarts);
        }


        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddShoppingСarts([FromForm] string VariantId)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                if (db.ShoppingСarts.Where(x => x.UserId == user.Id && x.VariantId == VariantId) != null)
                {
                    db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = VariantId });
                    db.SaveChanges();
                    return Ok();
                }
                else return BadRequest("У пользователя в корзине уже имеется данный товар");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Ошибка сервера");
            }
        }

        [Route("AddList")]
        [HttpPost]
        public async Task<IActionResult> AddListShoppingСarts([FromForm] List<string> VariantId)
        {
            try
            {
                string username = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);
                bool error = false;
                foreach (var item in VariantId)
                {
                    if (db.ShoppingСarts.Where(x => x.UserId == user.Id && x.VariantId == item) != null)
                    {
                        db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = item });
                    }
                    else error = true;
                }
                db.SaveChanges();
                if (!error) return Ok();
                else return BadRequest("Один или несколько товаров уже лежали в корзине");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest("Ошибка сервера");
            }
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> EditShoppingСarts([FromForm] string ShoppingСartId, [FromForm] int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    ShoppingСart? shoppingCart = db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).FirstOrDefault();
                    if (shoppingCart == null)
                    {
                        shoppingCart.quantity = quantity;
                        db.SaveChanges();

                        return Ok((quantity,true));
                    }
                    else return BadRequest("Позиция не найдена.");
                }
                return BadRequest("Количество продукции должно быть положительным и не равным нул.");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [Route("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteShoppingСarts([FromForm] string ShoppingСartId)
        {
            var rez = await db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).ExecuteDeleteAsync();
            db.SaveChanges();

            return Ok("ок");
        }
    }
}
