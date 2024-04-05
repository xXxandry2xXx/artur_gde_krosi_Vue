using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService
{
    public class ShoppingCartService
    {
        private readonly ApplicationIdentityContext db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartService(ApplicationIdentityContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }
        public async Task<List<ShoppingCartView>> GetShoppingСarts(string name)
        {
            string username = name;/*HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            var shoppingСarts = db.ShoppingСarts.Include(x => x.Variant).Select(x => MapGetShoppingСarts(x,x.Variant.quantityInStock > x.quantity)).AsNoTracking().ToList();
            return shoppingСarts;
        }
        private static ShoppingCartView MapGetShoppingСarts(ShoppingСart shoppingСart,bool availability)
        {
            ShoppingCartView shoppingCartView = new ShoppingCartView(shoppingСart.ShoppingСartId,shoppingСart.quantity, availability);
            return shoppingCartView;
        }
        public async Task AddShoppingСarts(string name, string VariantId)
        {
            string username = name;/*HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            if (!db.ShoppingСarts.Any(x => x.UserId == user.Id && x.VariantId == VariantId))
            {
                db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = VariantId, quantity = 1 });
                db.SaveChanges();
                return;
            }
            else throw new ArgumentException("У пользователя в корзине уже имеется данный товар");
        }
        public async Task AddListShoppingСarts(string name, List<string> VariantId)
        {
            string username = name;/* HttpContext.User.Identity.Name;*/
            var user = await _userManager.FindByNameAsync(username);
            bool error = false;
            foreach (var item in VariantId)
            {
                if (!db.ShoppingСarts.Any(x => x.UserId == user.Id && x.VariantId == item))
                {
                    db.ShoppingСarts.Add(new ShoppingСart { UserId = user.Id, VariantId = item, quantity = 1 });
                }
                else error = true;
            }
            db.SaveChanges();
            if (!error) return;
            else throw new ArgumentException("Один или несколько товаров уже лежали в корзине");
        }
        public async Task<int> EditShoppingСarts( string ShoppingСartId, int quantity)
        {
            if (quantity > 0)
            {
                ShoppingСart? shoppingCart = db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).FirstOrDefault();
                if (shoppingCart != null)
                {
                    shoppingCart.quantity = quantity;
                    db.SaveChanges();
                    return quantity;
                }
                else throw new ArgumentException("Позиция не найдена.");
            }
            throw new ArgumentException("Количество продукции должно быть положительным и не равным нул.");
        }
        public async Task DeleteShoppingСarts( string ShoppingСartId)
        {
            var rez = await db.ShoppingСarts.Where(x => x.ShoppingСartId == ShoppingСartId).ExecuteDeleteAsync();
            db.SaveChanges();
        }
    }
}
