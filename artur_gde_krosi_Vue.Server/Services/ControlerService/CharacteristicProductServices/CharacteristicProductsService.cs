using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Google.Protobuf.WellKnownTypes;
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
        public string AddCharacteristicProducts(string ProductId, string name)
        {
            db.CharacteristicProducts.Add(new CharacteristicProduct
            {
                name = name, 
                ProductId = ProductId
            });
             db.SaveChanges();
            return db.CharacteristicProducts.Where(x => x.name == name && x.ProductId == ProductId).FirstOrDefault().CharacteristicProductId;

        }
        public async Task EditCharacteristicProducts(string CharacteristicProductId, string name)
        { 
            CharacteristicProduct characteristic = db.CharacteristicProducts.Where(x => x.CharacteristicProductId == CharacteristicProductId).FirstOrDefault();
            if (characteristic == null) throw new ArgumentException("характеристика не найдена");
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
