using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews.ProductVies;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.ControlerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static artur_gde_krosi_Vue.Server.Models.moyskladApi.VariantApi;
using Product = artur_gde_krosi_Vue.Server.Models.BdModel.Product;

namespace artur_gde_krosi_Vue.Server.Controller;

[Route("api/[controller]/")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    private readonly ProductService _product;

    public ProductController(ILogger<ProductController> logger, ProductService product)
    {
        _logger = logger;
        _product = product;
    }

    [HttpGet]
    [Route("GetProduct")]
    public async Task<IActionResult> GetProduct(string ProductId)
    {
        Product product = await _product.GetProduct(ProductId);
        return Ok(product);
    }
    //[HttpGet]
    //[Route("GetProductSearch")]
    //public async Task<IActionResult> GetProductSearch(string strSearch)
    //{
    //    var product = db.Products.Where(X => X.name.Contains(strSearch)).Select(x => new { name = x.name }).AsNoTracking().Take(10);
    //    return Ok(product);
    //}
    [HttpGet]
    [Route("GetAllProductSearch")]
    public async Task<IActionResult> GetAllProductSearch()
    {
        List<AllProductSearchViews> product = _product.GetAllProductSearch();
        return Ok(product);
    }

    [HttpGet]
    [Route("Variant")]
    public async Task<IActionResult> Variant([FromHeader] string VariantId)
    {
        Variant? variant = _product.GetVariant(VariantId);
        return Ok(variant);
    }

    [Route("GetProductList")]
    [HttpGet]
    public async Task<IActionResult> GetProductsList([FromHeader] int priseDown = -1, [FromHeader] int priseUp = -1, [FromHeader] List<string> brendsIds = null, [FromHeader] List<string> modelKrosovocksIds = null,
        [FromHeader] List<double> shoeSizesChecked = null, [FromHeader] bool availability = false,
        [FromHeader] string PlaceholderContent = null, [FromHeader] SortState sortOrder = SortState.NameAsc, [FromHeader] int pageProducts = 1)
    {
        ProductListView productListView = _product.GetProductList(priseDown, priseUp, brendsIds, modelKrosovocksIds,
            shoeSizesChecked, availability, PlaceholderContent, sortOrder, pageProducts);
        return Ok(productListView);
    }
}

