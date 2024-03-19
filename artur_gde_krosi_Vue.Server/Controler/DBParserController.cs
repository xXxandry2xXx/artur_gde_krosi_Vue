using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Quartz;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controler
{
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]/")]
    [ApiController]
    public class DBParserController : ControllerBase
    {
        private readonly ILogger<FilterController> _logger;
        ApplicationIdentityContext db;
        private readonly IScheduler _scheduler;

        public DBParserController(ILogger<FilterController> logger, ApplicationIdentityContext context, IScheduler scheduler)
        {
            _logger = logger;
            db = context;
            _scheduler = scheduler;
        }

        [Route("Parse")]
        [HttpGet]
        public async Task<IActionResult> Parser()
        {
            try
            {
                // Находим нужное задание по его ключу
                var jobKey = new JobKey("artur_gde_krosi_Vue.Server.Schedulers.ProductAndGroupJob", "DEFAULT");
                var jobDetail = await _scheduler.GetJobDetail(jobKey);

                if (jobDetail == null)
                {
                    return NotFound("Задание не найдено");
                }

                // Запускаем задание
                await _scheduler.TriggerJob(jobKey);

                return Ok("Задание успешно запущено");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка: {ex.Message}");
            }

            ProductApi productApi = new ProductApi();
            getApiRequest<ProductApi.Root> requestProduct = new getApiRequest<ProductApi.Root>();
            productApi.root = await requestProduct.GetApiReqesi(productApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/product?expand=images.miniature.href,productFolder");

            GroupApi groupApi = new GroupApi();
            getApiRequest<GroupApi.Root> requestGroup = new getApiRequest<GroupApi.Root>();
            groupApi.root = await requestGroup.GetApiReqesi(groupApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/productfolder?expand=productFolder");

            VariantApi variantApi = new VariantApi();
            getApiRequest<VariantApi.Root> requestVariant = new getApiRequest<VariantApi.Root>();
            variantApi.root = await requestVariant.GetApiReqesi(variantApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/variant?expand=rows.images,product");

            var serviceProviderWithLogger = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .BuildServiceProvider();

            var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();

            loggerFactory.AddProvider(new LoggerProvider());

            db.Brends.RemoveRange(db.Brends.ToList());
            db.ModelKrosovocks.RemoveRange(db.ModelKrosovocks.ToList());
            db.Products.RemoveRange(db.Products.ToList());
            db.Variants.RemoveRange(db.Variants.ToList());

            Console.WriteLine("после");

            List<Brend> brends = new();
            foreach (var item in groupApi.root.rows)
            {
                if (item.productFolder == null)
                {
                    brends.Add(new Brend()
                    {
                        BrendId = item.id,
                        name = item.name,
                        ModelKrosovocks = new()
                    });
                }
            }

            List<ModelKrosovock> modelKrosovoks = new();
            foreach (var item in groupApi.root.rows)
            {
                if (item.productFolder != null && brends.Find(x => x.BrendId == item.productFolder.id) != null)
                {
                    modelKrosovoks.Add(new ModelKrosovock()
                    {
                        ModelKrosovockId = item.id,
                        name = item.name,
                        BrendID = item.productFolder.id
                    });
                }
            }

            List<Product> products = new();
            List<Image> images = db.Images.ToList();
            foreach (var item in productApi.root.rows)
            {
                if (item.productFolder != null && modelKrosovoks.Find(x => x.ModelKrosovockId == item.productFolder.id) != null)
                {
                    products.Add(new Product()
                    {
                        ProductId = item.id,
                        name = item.name,
                        ModelKrosovockId = item.productFolder.id,
                        description = item.description
                    });
                    int index = 0;
                    foreach (var itemImg in item.images.rows)
                    {
                        if (images.Find(x => x.ImageId == itemImg.title) == null)
                        {
                            byte[]? imageBytes = null;

                            using (HttpClient client = new HttpClient())
                            {
                                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer ad4bc311f51bafdc7357e20ece905d282f6fe448");
                                HttpResponseMessage response = await client.GetAsync(itemImg.meta.downloadHref);

                                if (response.IsSuccessStatusCode)
                                {
                                    imageBytes = await response.Content.ReadAsByteArrayAsync();
                                }
                                else
                                {
                                    Console.WriteLine($"Ошибка: {response.StatusCode} - {response.ReasonPhrase}");
                                }
                            }
                            try
                            {
                                AmazonS3Config configsS3 = new AmazonS3Config
                                {
                                    ServiceURL = "https://s3.yandexcloud.net"
                                };

                                using (var client = new AmazonS3Client("YCAJE48UBzauWHZirRHnF-WGB", "YCP8Jh2UT_grJjhcADcciAW0BUA_GW86OuxDr53d", configsS3)) // Укажите соответствующий регион
                                {
                                    var request = new PutObjectRequest
                                    {
                                        BucketName = "bucetimg",
                                        Key = itemImg.title,
                                        InputStream = new MemoryStream(imageBytes)
                                    };

                                    try
                                    {
                                        await client.PutObjectAsync(request);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                }
                                images.Add(new Image()
                                {
                                    ImageId = itemImg.title,
                                    Index = index,
                                    ImageSrc = "https://bucetimg.storage.yandexcloud.net/" + itemImg.title,
                                    ProductId = products.Find(X => X.ProductId == item.id).ProductId
                                });
                                index++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                throw;
                            }
                        }
                    }
                }

            }

            List<Variant> variants = new();
            foreach (var item in variantApi.root.rows)
            {
                if (item.product != null && products.Find(x => x.ProductId == item.product.id) != null)
                {
                    if (item.characteristics[0].value.Contains("."))
                    {
                        item.characteristics[0].value = item.characteristics[0].value.Replace(".", ",");
                    }
                    variants.Add(new Variant()
                    {
                        VariantId = item.id,
                        shoeSize = Convert.ToDouble(item.characteristics[0].value),
                        prise = item.salePrices[0].value,
                        externalCode = item.externalCode,
                        ProductId = item.product.id,
                        quantityInStock = 0
                    });
                }
            }
            try
            {
                db.Brends.AddRange(brends);
                db.ModelKrosovocks.AddRange(modelKrosovoks);
                db.Products.AddRange(products);
                db.Variants.AddRange(variants);
                db.Images.AddRange(images);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

