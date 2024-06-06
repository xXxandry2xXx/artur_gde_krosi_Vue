using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static artur_gde_krosi_Vue.Server.Models.ContelerViews.ShoppingCartView;

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
        public async Task<ShoppingCartView> GetShoppingСarts(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) throw new ArgumentException("Данного пользователя не существует");
            ShoppingCartView shoppingCartView = new ShoppingCartView();
            shoppingCartView.shoppingCartList = db.ShoppingСarts.Include(x => x.Variant).Select(x => MapGetShoppingСarts(x,x.Variant.quantityInStock > x.quantity)).AsNoTracking().ToList();
            shoppingCartView.totalPrise = shoppingCartView.shoppingCartList.Sum(x => x.prise);
            return shoppingCartView;
        }
        private static ShoppingCartView.ShoppingCartListElement MapGetShoppingСarts(ShoppingСart shoppingСart,bool availability)
        {
            ShoppingCartView.ShoppingCartListElement shoppingCartView = new ShoppingCartView.ShoppingCartListElement(shoppingСart.ShoppingСartId, shoppingСart.quantity, availability, shoppingСart.VariantId,shoppingСart.Variant.ProductId, shoppingСart.Variant.prise);
            return shoppingCartView;
        }
        public async Task AddShoppingСarts(string name, string VariantId)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) throw new ArgumentException("Данного пользователя не существует");
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
            var user = await _userManager.FindByNameAsync(name);
            if (user == null) throw new ArgumentException("Данного пользователя не существует");
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
