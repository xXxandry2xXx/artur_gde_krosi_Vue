using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.EntityFrameworkCore;
using Product = artur_gde_krosi_Vue.Server.Models.BdModel.Product;
using System.Drawing.Drawing2D;

namespace artur_gde_krosi_Vue.Server.Services.Parser;

public class ProductParserService : IProductParserService
{
    private readonly IPostImegesS3Service _postImegesS3Service;
    public ProductParserService( IPostImegesS3Service postImegesS3Service)
    {
        _postImegesS3Service = postImegesS3Service;
    }

    public async Task<List<Product>> ProductPars(ApplicationIdentityContext _db ,ProductApi productApi, List<ModelKrosovock> modelKrosovoks)
    {
        List<Product> products = _db.Products.ToList();
        List<Image> images = _db.Images.ToList();
        getApiRequest<StockApi.Root> requestVariantStok = new getApiRequest<StockApi.Root>();
        foreach (var item in productApi.root.rows)
        {
            if (item.productFolder != null && modelKrosovoks.Any(x => x.ModelKrosovockId == item.productFolder.id))
            {
                if (!products.Any(x => x.ProductId == item.id))
                {
                    _db.Products.Add(new Product()
                    {
                        ProductId = item.id,
                        name = item.name,
                        ModelKrosovockId = item.productFolder.id,
                        description = item.description,
                        views = 0
                    });
                    products.Add(new Product() { ProductId = item.id });
                }
                else
                {
                    Product product = products.Find(x => x.ProductId == item.id);
                    if (product.ProductId == item.id &&
                        product.name == item.name &&
                        product.ModelKrosovockId == item.productFolder.id &&
                        product.description == item.description &&
                        product.views == 0)
                    {
                        _db.Products.Update(new Product()
                        {
                            ProductId = item.id,
                            name = item.name,
                            ModelKrosovockId = item.productFolder.id,
                            description = item.description,
                            views = 0
                        });
                    }
                }
                int index = 0;
                foreach (var itemImg in item.images.rows)
                {
                    if (!images.Any(x => x.ImageId == itemImg.title))
                    {
                        await _postImegesS3Service.PostImageS3Reqesi(itemImg, index, item.id);
                        index++;
                    }
                }
            }
        }
        for ( int i = 0; i < products.Count; i++)
        {
            if (!productApi.root.rows.Any(x => x.id == products[i].ProductId))
            {
                _db.Products.Where(x => x.ProductId == products[i].ProductId).ExecuteDelete();
                products.RemoveAt(i);
            }
        }
        Console.WriteLine("конец 2 - 1");
        return products;
    }

    public async Task VariantPars(ApplicationIdentityContext _db,  VariantApi variantApi, StockApi stockApi, List<Product> products)
    {
        List<Variant> variants = _db.Variants.ToList();
        foreach (var item in variantApi.root.rows)
        {
            if (item.product != null && products.Find(x => x.ProductId == item.product.id) != null)
            {
                if (item.characteristics[0].value.Contains("."))
                {
                    item.characteristics[0].value = item.characteristics[0].value.Replace(".", ",");
                }
                string? stock = stockApi.root.rows.Any(x => x.externalCode == item.externalCode) ?  stockApi.root.rows.Find(x => x.externalCode == item.externalCode).stock : "0";
                if (stock.Contains("."))
                {
                    stock = stock.Split('.')[0];
                }
                if (!variants.Any(x => x.VariantId == item.id))
                {
                    _db.Variants.Add(new Variant()
                    {
                        VariantId = item.id,
                        shoeSize = Convert.ToDouble(item.characteristics[0].value),
                        prise = item.salePrices[0].value,
                        externalCode = item.externalCode,
                        ProductId = item.product.id, 
                        quantityInStock =  Convert.ToInt32(stock) 
                    });
                }
                else
                {
                    Variant variant = variants.Find(x => x.VariantId == item.id);
                    if (variant.VariantId == item.id &&
                        variant.shoeSize == Convert.ToDouble(item.characteristics[0].value) &&
                        variant.prise == item.salePrices[0].value &&
                        variant.externalCode == item.externalCode &&
                        variant.ProductId == item.product.id &&
                        variant.quantityInStock == Convert.ToInt32(stock))
                    {
                        _db.Variants.Update(new Variant()
                        {
                            VariantId = item.id,
                            shoeSize = Convert.ToDouble(item.characteristics[0].value),
                            prise = item.salePrices[0].value,
                            externalCode = item.externalCode,
                            ProductId = item.product.id,
                            quantityInStock = Convert.ToInt32(stock)
                        });
                    }
                }
            }
        }
        for (int i = 0; i < variants.Count; i++)
        {
            if (!variantApi.root.rows.Any(x => x.id == variants[i].VariantId))
            {
                _db.Variants.Where(x => x.VariantId == variants[i].VariantId).ExecuteDelete();
            }
        }
        Console.WriteLine("конец 2 - 2");
    }

    public async Task QuantityInStockPars(ApplicationIdentityContext _db, StockApi stockApi)
    {
        List<Variant> variants = _db.Variants.ToList();
        foreach (var item in stockApi.root.rows)
        {
            if (variants.Any(x => x.externalCode == item.externalCode))
            {
                if (item.stock.Contains("."))
                {
                    item.stock = item.stock.Split('.')[0];
                }
                try
                {
                    _db.Variants.FirstOrDefault(x => x.externalCode == item.externalCode).quantityInStock = Convert.ToInt32(item.stock);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "");
                }
            }
        }
    }

}

