using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews.ProductVies;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Yandex.Cloud.Cdn.V1;
using static artur_gde_krosi_Vue.Server.Models.ContelerViews.ProductVies.ProductListView;
using Variant = artur_gde_krosi_Vue.Server.Models.BdModel.Variant;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService;

public class ProductService
{
    private readonly ApplicationIdentityContext db;
    private readonly IMemoryCache _cache;

    public ProductService(ApplicationIdentityContext _db, IMemoryCache cache)
    {
        db = _db;
        _cache = cache;
    }
    public async Task<Product> GetProduct(string ProductId)
    {
        Product? product = db.Products.Where(X => X.ProductId == ProductId).AsNoTracking()
                               .Include(x => x.ModelKrosovock).ThenInclude(x => x.Brend)
                               .Include(x => x.Images)
                               .Include(x => x.Variants)
                               .Include(x => x.CharacteristicProducts).ThenInclude(x => x.CharacteristicProductValues).FirstOrDefault();
        db.Products.Where(x => x.ProductId == ProductId).ExecuteUpdateAsync(x => x.SetProperty(y => y.views, y => y.views + 1));
        return product;
    }
    public Variant GetVariant( string VariantId)
    {
        Variant? variant = db.Variants.Where(x => x.VariantId == VariantId).FirstOrDefault();
        return variant;
    }
    public List<AllProductSearchViews> GetAllProductSearch()
    {
        string cacheKey = "AllProduct";
        if (!_cache.TryGetValue(cacheKey, out List<AllProductSearchViews> allProduct))
        {
            allProduct = db.Products.Include(x => x.Images.Where(x => x.Index == 0)).Select(x => MapAllProductSearch(x)).AsNoTracking().ToList();
            _cache.Set(cacheKey, allProduct, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12) });
        }
        return allProduct;
    }
    public static AllProductSearchViews MapAllProductSearch(Product product)
    {
        AllProductSearchViews allProductSearchViews = new AllProductSearchViews(product.ProductId,
            product.name,
            product.Images[0].ImageSrc);
        return allProductSearchViews;
    }
    public ProductListView GetProductList(int priseDown, int priseUp, List<string> brendsIds, List<string> modelKrosovocksIds,
        List<double> shoeSizesChecked, bool availability,
        string PlaceholderContent, SortState sortOrder, int pageProducts)
    {
        if (PlaceholderContent != null) PlaceholderContent = PlaceholderContent.Trim().ToLower();
        string[] PlaceholderArry = null;
        if (PlaceholderContent != null && PlaceholderContent != "") PlaceholderArry = PlaceholderContent.Split(' ');
        IQueryable<Models.BdModel.Product> products = db.Products.Include(x => x.Variants)
            .Include(x => x.ModelKrosovock).ThenInclude(x => x.Brend)
            .Include(x => x.Images.Where(x => x.Index == 0)).AsNoTracking();

        //фильтрация по диапазону цен
        if (priseUp != -1 && priseDown != -1 && priseDown <= priseUp)
            products = products.Where(x => priseDown * 100 <= x.Variants[0].prise && priseUp * 100 >= x.Variants[0].prise);
        //фильтрация по Размеру и наличию 
        if (shoeSizesChecked != null && shoeSizesChecked.Count != 0)
        {
            if (availability)
                products = products.Where(x => x.Variants.Any(y => y.quantityInStock != 0));
            else
                products = products.Where(x => shoeSizesChecked.Any(y => x.Variants.Any(z => z.shoeSize == y && (!availability || z.quantityInStock != 0))));
        }

        //фильтрация по Брендам
        if (brendsIds != null && brendsIds.Count != 0)
            products = products.Where(x => brendsIds.Any(y => x.ModelKrosovock.Brend.BrendId == y));
        //фильтрация по Моделям
        if (modelKrosovocksIds != null && modelKrosovocksIds.Count != 0)
            products = products.Where(x => modelKrosovocksIds.Any(y => x.ModelKrosovock.ModelKrosovockId == y));
        //фильтрация по Поиску  
        if (PlaceholderContent != null && PlaceholderContent != "")
            products = products.Where(x => PlaceholderArry.All(y => (x.ModelKrosovock.name.ToLower() + " " + x.ModelKrosovock.Brend.name.ToLower() + " " + x.name.ToLower()).Contains(y)));
        //сортировка
        switch (sortOrder)
        {
            case SortState.NameAsc:
                products = products.OrderBy(x => x.name);
                break;
            case SortState.NameDesc:
                products = products.OrderByDescending(x => x.name);
                break;
            case SortState.PriseAsc:
                products = products.OrderBy(x => x.Variants[0].prise);
                break;
            case SortState.PriseDesc:
                products = products.OrderByDescending(x => x.Variants[0].prise);
                break;
            case SortState.PopularityAsc:
                products = products.OrderBy(x => x.views);
                break;
            case SortState.PopularityDesc:
                products = products.OrderByDescending(x => x.views);
                break;
            default:
                break;
        };
        ProductListView productLV = new ProductListView();
        productLV.productList = products
            .Skip(20 * (pageProducts - 1)).Take(20)
            .Select(x => MapProductList(x)).ToList();
        if (productLV.productList.Count == 0)
        {
            productLV.Count = 0;
        }
        else
        {
            productLV.Count = products.Count();
        }
        return productLV;
    }
    private static ProductListView.ProductList MapProductList(Product product)
    {
        ProductListView.ProductList productList = new ProductListView.ProductList(product.name,
            product.ModelKrosovock.name, product.ModelKrosovock.Brend.name,
            product.views, product.ProductId,
            product.Variants.Select(x => MapVariants(x)).ToList(), product.Images[0].ImageSrc);
        return productList;
    }
    private static ProductListView.Variant MapVariants(Variant variant)
    {
        ProductListView.Variant variantLV = new ProductListView.Variant(variant.VariantId,
            variant.shoeSize, variant.quantityInStock, variant.prise);
        return variantLV;
    }
}

