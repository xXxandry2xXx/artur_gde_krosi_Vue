using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controler.CharacteristicProductFlouder
{
    public class CharacteristicProductValueControler : ControllerBase
    {
        private readonly ILogger<CharacteristicProductValueControler> _logger;
        ApplicationIdentityContext db;

        public CharacteristicProductValueControler(ILogger<CharacteristicProductValueControler> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        [Route("/AddCharacteristicProductsValue")]
        public async Task<IActionResult> AddCharacteristicProductsValue(string value, string CharacteristicProductId)
        {
            try
            {
                db.CharacteristicProductValues.Add(new CharacteristicProductValue
                {
                    Value = value,
                    CharacteristicProductId = CharacteristicProductId
                });
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("/DeleteCharacteristicProductsValue")]
        public async Task<IActionResult> DeleteCharacteristicProductsValue(string CharacteristicProductValueId)
        {
            try
            {
                await db.CharacteristicProductValues.Where(x => x.CharacteristicProductValueId == CharacteristicProductValueId).ExecuteDeleteAsync();
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return BadRequest();
            }

        }
    }
}
