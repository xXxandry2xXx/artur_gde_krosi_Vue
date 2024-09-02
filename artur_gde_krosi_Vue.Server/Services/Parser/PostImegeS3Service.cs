using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using System.Configuration;
using artur_gde_krosi_Vue.Server.Contracts.Services.Parser;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public class PostImegesS3Service : IPostImegesS3Service
    {
        private readonly ApplicationIdentityContext db;
        private readonly IConfiguration configuration;

        public PostImegesS3Service(ApplicationIdentityContext db, IConfiguration config)
        {
            this.db = db;
            configuration = config;
        }
        public async Task PostImageS3Reqesi(ProductApi.Row itemImg, int index,  string ProductId)
        {
            byte[]? imageBytes = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + configuration.GetSection("MySkladApi:key").Value);
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
                using (var client = new AmazonS3Client(configuration.GetSection("S3Bucket:idToken").Value, configuration.GetSection("S3Bucket:token").Value, configsS3)) // Укажите соответствующий регион
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
                db.Images.Add(new Image()
                {
                    ImageId = itemImg.title,
                    Index = index,
                    ImageSrc = "https://bucetimg.storage.yandexcloud.net/" + itemImg.title,
                    ProductId = ProductId
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
