using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller
{
    //[Authorize(Roles = "User")]
    [Route("api/[controller]/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        ApplicationIdentityContext db;

        public ProductController(ILogger<ProductController> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetProdut(string ProductId)
        {
            var product = db.Products.Where(X => X.ProductId == ProductId)
                .Include(x => x.ModelKrosovock.Brend)
                .Include(x => x.Images)
                .Include(x => x.Variants)
                .Include(x => x.CharacteristicProducts).ThenInclude(x => x.CharacteristicProductValues);

            return Ok(product);
        }
        [Route("GetProductList")]
        [HttpPost]
        public async Task<IActionResult> GetProdutsList([FromForm] int priseDown = 0, [FromForm] int priseUp = 0, [FromForm] List<string> brendsIds = null, [FromForm] List<string> modelKrosovocksIds = null,
            [FromForm] List<double> shoeSizesChecked = null, [FromForm] bool availability = false,
            [FromForm] string PlaceholderContent = null, [FromForm] SortState sortOrder = SortState.NameAsc, [FromForm] int pageProducts = 1)
        {

            if (PlaceholderContent != null) { PlaceholderContent = PlaceholderContent.Trim().ToLower(); }
            string[] PlaceholderArry = null;
            if (PlaceholderContent != null && PlaceholderContent != "") { PlaceholderArry = PlaceholderContent.Split(' '); }
            //List<double> shoeSizes = db.Variants.Select(x => x.shoeSize).Distinct().ToList();
            var products = db.Products.Include(x => x.ModelKrosovock.Brend)
                .Include(x => x.Variants)
                .Include(x => x.Images.Where(x => x.Index == 0))
                //сортировка по Брендам
                .Where(x => (brendsIds == null || brendsIds.Count == 0)
                    || brendsIds.Any(y => x.ModelKrosovock.Brend.BrendId == y))
                //сортировка по Моделям
                .Where(x => (modelKrosovocksIds == null || modelKrosovocksIds.Count == 0)
                    || modelKrosovocksIds.Any(y => x.ModelKrosovock.ModelKrosovockId == y))
                //сортировка по диапазону цен
                .Where(x => (priseUp == 0 || priseDown == 0) || (priseDown > priseUp)
                    || (priseDown * 100 <= x.Variants[0].prise && priseUp * 100 >= x.Variants[0].prise))
                //сортировка по Поиску 
                .Where(x => (PlaceholderContent == null || PlaceholderContent == "")
                    || PlaceholderArry.All(y => (x.ModelKrosovock.name.ToLower() + " " + x.ModelKrosovock.Brend.name.ToLower() + " " + x.name.ToLower()).Contains(y)))
                //сортировка по Размеру и наличию 
                .Where(x => ((shoeSizesChecked == null || shoeSizesChecked.Count == 0)
                    && availability && x.Variants.Any(y => y.quantityInStock != 0))
                    || ((shoeSizesChecked == null || shoeSizesChecked.Count == 0) && !availability
                        || shoeSizesChecked.Any(y => x.Variants.Any(z => z.shoeSize == y && (!availability || z.quantityInStock != 0)))))
                .Select(x => new
                {
                    ProductId = x.ProductId,
                    name = x.name,
                    Variants = x.Variants.Select(y => new
                    {
                        shoeSize = y.shoeSize,
                        quantityInStock = y.quantityInStock,
                        prise = y.prise
                    }).ToList(),
                    ModelKrosovock_Name = x.ModelKrosovock.name,
                    Brend_Name = x.ModelKrosovock.Brend.name,
                    Images = x.Images.Where(y => y.Index == 0).Take(1).Select(x => new
                    {
                        ImgSrc = x.ImageSrc
                    })
                })
                .ToList();
            switch (sortOrder)
            {
                case SortState.NameAsc:
                    products = products.OrderBy(x => x.name).ToList();
                    break;
                case SortState.NameDesc:
                    products = products.OrderByDescending(x => x.name).ToList();
                    break;
                case SortState.PriseAsc:
                    products = products.OrderBy(x => x.Variants[0].prise).ToList();
                    break;
                case SortState.PriseDesc:
                    products = products.OrderByDescending(x => x.Variants[0].prise).ToList();
                    break; 
                default:
                    break;
            }
            if (products.Count == 0)
            {
                var resultNon = new
                {
                    Products = products,
                    priseMin = 0,
                    priseMax = 0,
                    productcount = 0
                };
                return Ok(resultNon);
            }
            int priseMin = (int)products.Min(x => x.Variants[0].prise) / 100;
            int priseMax = (int)products.Max(x => x.Variants[0].prise) / 100;
            int ProductCount = products.Count();
            var result = new
            {
                Products = products.Skip(20 * (pageProducts - 1)).Take(20),
                priseMin = priseMin,
                priseMax = priseMax,
                productcount = ProductCount
            };
            return Ok(result);
        }
    }
}
