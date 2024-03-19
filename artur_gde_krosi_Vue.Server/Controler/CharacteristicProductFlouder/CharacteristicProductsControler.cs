using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controler.CharacteristicProductFlouder
{
    [ApiController]
    public class CharacteristicProductsControler : ControllerBase
    {
        private readonly ILogger<CharacteristicProductsControler> _logger;
        ApplicationIdentityContext db;

        public CharacteristicProductsControler(ILogger<CharacteristicProductsControler> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context; 
        }
        [HttpGet]
        [Route("/AddCharacteristicProducts")]
        public async Task<IActionResult> AddCharacteristicProducts(string name, string ProductId)
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
        [HttpGet]
        [Route("/EditCharacteristicProducts")]
        public async Task<IActionResult> EditCharacteristicProducts(string name, string ProductId)
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

        [HttpGet]
        [Route("/DeleteCharacteristicProducts")]
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
