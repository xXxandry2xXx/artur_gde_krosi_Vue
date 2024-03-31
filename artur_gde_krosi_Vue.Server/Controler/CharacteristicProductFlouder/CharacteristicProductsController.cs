using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller.CharacteristicProductFolder
{
    //[Authorize(Roles = "Manager")]
    [Route("api/CharacteristicProductFolder/[controller]")]
    [ApiController]
    public class CharacteristicProductsController : ControllerBase
    {
        private readonly ILogger<CharacteristicProductsController> _logger;
        ApplicationIdentityContext db;

        public CharacteristicProductsController(ILogger<CharacteristicProductsController> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacteristicProducts(string ProductId, string name)
        {
            try
            {
                db.CharacteristicProducts.Add(new CharacteristicProduct
                {
                    name = name,
                    ProductId = ProductId
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
        public async Task<IActionResult> EditCharacteristicProducts(string CharacteristicProductId, string name)
        {
            try
            {
                CharacteristicProduct characteristic = db.CharacteristicProducts.Where(x => x.CharacteristicProductId == CharacteristicProductId).FirstOrDefault();
                if (characteristic != null)
                {
                    characteristic.name = name;
                }
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                throw Ex;
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharacteristicProducts(string CharacteristicProductId)
        {
            try
            {
                await db.CharacteristicProducts.Where(x => x.CharacteristicProductId == CharacteristicProductId).ExecuteDeleteAsync();
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
