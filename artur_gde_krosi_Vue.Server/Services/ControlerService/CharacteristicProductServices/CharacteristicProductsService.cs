using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices
{
    public class CharacteristicProductsService
    {
        private readonly ApplicationIdentityContext db;

        public CharacteristicProductsService(ApplicationIdentityContext db)
        {
            this.db = db;
        }
        public async Task AddCharacteristicProducts(string ProductId, string name)
        {
            db.CharacteristicProducts.Add(new CharacteristicProduct
            {
                name = name,
                ProductId = ProductId
            });
            await db.SaveChangesAsync();
        }
        public async Task EditCharacteristicProducts(string CharacteristicProductId, string name)
        {
            CharacteristicProduct characteristic = db.CharacteristicProducts.Where(x => x.CharacteristicProductId == CharacteristicProductId).FirstOrDefault();
            if (characteristic != null) throw new ArgumentException("характеристика не найдена");
            characteristic.name = name;
            await db.SaveChangesAsync();
        }
        public async Task DeleteCharacteristicProducts(string CharacteristicProductId)
        {
            await db.CharacteristicProducts.Where(x => x.CharacteristicProductId == CharacteristicProductId).ExecuteDeleteAsync();
            await db.SaveChangesAsync();
        }

    }
}
