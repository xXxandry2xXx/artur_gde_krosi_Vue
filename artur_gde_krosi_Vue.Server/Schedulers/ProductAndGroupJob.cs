using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Quartz;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Policy;
using System.Net;
using Azure;
using System.Net.Http.Headers;
using System;
using System.Text;
using AspNetCore.Yandex.ObjectStorage.Configuration;
using AspNetCore.Yandex.ObjectStorage.Object.Responses;
using AspNetCore.Yandex.ObjectStorage;
using System.Drawing.Imaging;
using System.Drawing;
using Image = artur_gde_krosi_Vue.Server.Models.BdModel.Image;
using System.Configuration;
using Amazon.S3.Model;
using Amazon.S3;


namespace artur_gde_krosi_Vue.Server.Schedulers
{
    public class ProductAndGroupJob : IJob
    {
        private readonly IServiceProvider _provider;

        public ProductAndGroupJob(IServiceProvider provider)
        {
            _provider = provider;
        }

        public Task Execute(IJobExecutionContext context)
        {

            Console.WriteLine("цикл");
            try
            {
                Logs();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "");
            }

            return Task.CompletedTask;

        }
        public async Task Logs()
        {
            ProductApi productApi = new ProductApi();
            getApiRequest<ProductApi.Root> requestProduct = new getApiRequest<ProductApi.Root>();
            productApi.root = await requestProduct.GetApiReqesi(productApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/product?expand=images.miniature.href,productFolder");

            GroupApi groupApi = new GroupApi();
            getApiRequest<GroupApi.Root> requestGroup = new getApiRequest<GroupApi.Root>();
            groupApi.root = await requestGroup.GetApiReqesi(groupApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/productfolder?expand=productFolder");

            VariantApi variantApi = new VariantApi();
            getApiRequest<VariantApi.Root> requestVariant = new getApiRequest<VariantApi.Root>();
            variantApi.root = await requestVariant.GetApiReqesi(variantApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/variant?expand=rows.images,product");


            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();


                int width = 200;
                int height = 200;
                Color color = Color.Red;

                byte[] imageBytes2;

                // Создание нового изображения
                using (Bitmap bitmap = new Bitmap(width, height))
                {
                    // Заполнение всего изображения одним цветом
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        using (SolidBrush brush = new SolidBrush(color))
                        {
                            graphics.FillRectangle(brush, 0, 0, width, height);
                        }
                    }

                    // Кодируем изображение в формат PNG и записываем его в MemoryStream
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);
                        imageBytes2 = memoryStream.ToArray();
                    }
                }

                var credentials = new Amazon.Runtime.BasicAWSCredentials("YCAJEnMCyt7hPSgY7tU1BjQkN", "YCO21uxIJ0H2hn9jEIhGc71TnbKfmul9jlFO8qyU");

                AmazonS3Config configsS3 = new AmazonS3Config
                {
                    ServiceURL = "https://s3.yandexcloud.net"
                };

                using (var client = new AmazonS3Client("YCAJE48UBzauWHZirRHnF-WGB", "YCP8Jh2UT_grJjhcADcciAW0BUA_GW86OuxDr53d", configsS3)) // Укажите соответствующий регион
                {
                    var request = new GetObjectRequest
                    {
                        BucketName = "bucetimg",
                        Key = "bdbe5189ed262d3cc850ee3b2f54a524.jpeg" ,

                    };

                    try
                    {
                        using (var response = await client.GetObjectAsync(request))
                        using (var responseStream = response.ResponseStream)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                  await responseStream.CopyToAsync(memoryStream);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                    //var request = new PutObjectRequest
                    //{
                    //    BucketName = "bucetimg",
                    //    Key = "bdbe5189ed262d3cc850ee3b2f54a524.jpeg" ,

                    //};

                    //try
                    //{
                    //    await client.PutObjectAsync(request)
                    //    //using ( var response = await client.PutObjectAsync(request))
                    //    //using (var responseStream = response.ResponseStream)
                    //    //{
                    //    //    using (var memoryStream = new MemoryStream())
                    //    //    {
                    //    //        await responseStream.CopyToAsync(memoryStream);
                    //    //    }
                    //    //}
                    //}
                    //catch (Exception ex)
                    //{

                    //    throw;
                    //}
                }

                //var options = scope.ServiceProvider.Configure<YandexStorageOptions>(Configuration.GetSection("YandexStorageOptions"));


                

                var serviceProviderWithLogger = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole())
                    .BuildServiceProvider();

                var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();

                loggerFactory.AddProvider(new LoggerProvider());

                dbContext.Brends.RemoveRange(dbContext.Brends.ToList());
                dbContext.ModelKrosovocks.RemoveRange(dbContext.ModelKrosovocks.ToList());
                dbContext.Products.RemoveRange(dbContext.Products.ToList());
                dbContext.Variants.RemoveRange(dbContext.Variants.ToList());

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
                List<Image> images = dbContext.Images.ToList();
                foreach (var item in productApi.root.rows)
                {
                    if (item.productFolder != null && modelKrosovoks.Find(x => x.ModelKrosovockId == item.productFolder.id) != null)
                    {
                        products.Add(new Product()
                        {
                            ProductId = item.id,
                            name = item.name,
                            prise = 12,
                            ModelKrosovockId = item.productFolder.id
                        });
                        int index = 0;
                        foreach (var itemImg in item.images.rows)
                        {
                            if (images.Find(x => x.ImageId == itemImg.title) == null)
                            {
                                byte[]? imageBytes = null;
                                string url = itemImg.meta.downloadHref;

                                using (HttpClient client = new HttpClient())
                                {
                                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                                    client.DefaultRequestHeaders.Add("Authorization", "Bearer ad4bc311f51bafdc7357e20ece905d282f6fe448");
                                    HttpResponseMessage response = await client.GetAsync(url);

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
                                    images.Add(new Image()
                                    {
                                        ImageId = itemImg.title,
                                        Index = index,
                                        ImageData = imageBytes,
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
                    dbContext.Brends.AddRange(brends);
                    dbContext.ModelKrosovocks.AddRange(modelKrosovoks);
                    dbContext.Products.AddRange(products);
                    dbContext.Variants.AddRange(variants);
                    dbContext.Images.AddRange(images);

                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}


