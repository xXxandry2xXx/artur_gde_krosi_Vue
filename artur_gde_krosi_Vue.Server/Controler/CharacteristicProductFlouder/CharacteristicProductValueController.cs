using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller.CharacteristicProductFolder
{
    //[Authorize(Roles = "Manager")]
    [Route("api/CharacteristicProductFolder/[controller]")]
    [ApiController]
    public class CharacteristicProductValueController : ControllerBase
    {
        private readonly ILogger<CharacteristicProductValueController> _logger;
        ApplicationIdentityContext db;

        public CharacteristicProductValueController(ILogger<CharacteristicProductValueController> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpPost]
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
        [HttpPut]
        public async Task<IActionResult> EditCharacteristicProductsValue(string CharacteristicProductValueId, string value)
        {
            try
            {
                CharacteristicProductValue? characteristicProductValue =  db.CharacteristicProductValues.Where(x => x.CharacteristicProductValueId == CharacteristicProductValueId).FirstOrDefault();
                characteristicProductValue.Value = value;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return BadRequest();
            }

        }
        [HttpDelete]
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
