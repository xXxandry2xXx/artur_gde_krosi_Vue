using Amazon.Runtime.Internal.Util;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices
{
    public class CharacteristicProductValueService
    {
        private readonly ApplicationIdentityContext db;

        public CharacteristicProductValueService(ApplicationIdentityContext context)
        {
            db = context;
        }
        public string AddCharacteristicProductsValue(string value, string CharacteristicProductId)
        {
            db.CharacteristicProductValues.Add(new CharacteristicProductValue
            {
                Value = value,
                CharacteristicProductId = CharacteristicProductId
            });
            db.SaveChanges();
            return db.CharacteristicProductValues.Where(x => x.Value == value && x.CharacteristicProductId == CharacteristicProductId).FirstOrDefault().CharacteristicProductValueId;
        }
        public async Task EditCharacteristicProductsValue(string CharacteristicProductValueId, string value)
        {
            CharacteristicProductValue? characteristicProductValue = db.CharacteristicProductValues.Where(x => x.CharacteristicProductValueId == CharacteristicProductValueId).FirstOrDefault();
            characteristicProductValue.Value = value;
            await db.SaveChangesAsync();
        }
        public async Task DeleteCharacteristicProductsValue(string CharacteristicProductValueId)
        {
            await db.CharacteristicProductValues.Where(x => x.CharacteristicProductValueId == CharacteristicProductValueId).ExecuteDeleteAsync();
            await db.SaveChangesAsync();
        }
    }
}
