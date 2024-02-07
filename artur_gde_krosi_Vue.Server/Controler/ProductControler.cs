using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Azure.Identity;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Drawing;
using Image = System.Drawing.Image;


namespace artur_gde_krosi_Vue.Server.Controler
{
    [ApiController]
    [Route("/Produts")]
    public class ProductControler : ControllerBase
    {
        private readonly ILogger<ProductControler> _logger;
        ApplicationContext db;

        public ProductControler(ILogger<ProductControler> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        [Route("/Brends")]
        public async Task<IActionResult> GetBrends()
        {
            var Brends = db.Brends.ToList();

            return Ok(Brends);
        }
        [HttpGet]
        [Route("/ModelKrosovocks")]
        public async Task<IActionResult> GetModelKrosovocks()
        {
            var modelKrosovocks = db.ModelKrosovocks.ToList();

            return Ok(modelKrosovocks);
        }
        [HttpGet]
        [Route("/ImageProduct")]
        public async Task<IActionResult> GetImageProduct(string id)
        {
            return File(db.Images.FirstOrDefault(x => x.ProductId == id && x.Index == 0).ImageData, "image/jpeg");
        }
        [HttpGet]
        [Route("/ShoeSizes")]
        public async Task<IActionResult> GetShoeSizes(string id)
        {
            List<double> shoeSizes = db.Variants.Select(x => x.shoeSize).Distinct().ToList();

            return Ok(shoeSizes);
        }
        [HttpGet]
        public async Task<IActionResult> GetProduts()
        {
            var products = db.Products.Include(x => x.ModelKrosovock.Brend)
                 .Include(x => x.Variants)
                 .Include(x => x.Images.Where(x => x.Index == 0))
                 .Select(x => new
                 {
                     ProductId = x.ProductId,
                     name = x.name,
                     prise = x.prise,
                     Variants = x.Variants.Select(y => new
                     {
                         shoeSize = y.shoeSize,
                         quantityInStock = y.quantityInStock,
                         prise = y.prise
                     }).ToList(),
                     ModelKrosovock_Name = x.ModelKrosovock.name,
                     Brend_Name = x.ModelKrosovock.Brend.name,
                     Images = x.Images.OrderBy(y => y.ImageId).Take(1).Select(x => new
                     {
                         ImageId = x.ImageId
                     })
                 })
                 .ToList();
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
                Products = products.Take(20),
                priseMin = priseMin,
                priseMax = priseMax,
                productcount = ProductCount
            };
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetProduts(int priseDown = 0, int priseUp = 0, [FromQuery] List<string> brendsIds = null, [FromQuery] List<string> modelKrosovocksIds = null,
            [FromQuery] List<double> shoeSizesChecked = null, bool availability = false,
            string PlaceholderContent = null, SortState sortOrder = SortState.NameAsc, int pageProducts = 1)
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
                    prise = x.prise,
                    Variants = x.Variants.Select(y => new
                    {
                        shoeSize = y.shoeSize,
                        quantityInStock = y.quantityInStock,
                        prise = y.prise
                    }).ToList(),
                    ModelKrosovock_Name = x.ModelKrosovock.name,
                    Brend_Name = x.ModelKrosovock.Brend.name,
                    Images = x.Images.OrderBy(y => y.ImageId).Take(1).Select(x => new
                    {
                        ImageId = x.ImageId
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
