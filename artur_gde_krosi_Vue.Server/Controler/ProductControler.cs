using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controler
{
    public class ProductControler : ControllerBase
    {
        private readonly ILogger<ProductListControler> _logger;
        ApplicationIdentityContext db;

        public ProductControler(ILogger<ProductListControler> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        [Route("/Produt")]
        public async Task<IActionResult> GetProdut(string ProductId)
        {
            var product = db.Products.Where(X => X.ProductId == ProductId)
                .Include(x => x.ModelKrosovock.Brend)
                .Include(x => x.Images)
                .Include(x => x.Variants)
                .Include(x => x.CharacteristicProducts).ThenInclude(x => x.CharacteristicProductValues);

            return Ok(product);
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
