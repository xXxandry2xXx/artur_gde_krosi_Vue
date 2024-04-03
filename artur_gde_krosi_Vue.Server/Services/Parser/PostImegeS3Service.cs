using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Models.BdModel;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public class PostImegesS3Service : IPostImegesS3Service
    {
        private readonly ApplicationIdentityContext _db;

        public PostImegesS3Service(ApplicationIdentityContext db)
        {
            _db = db;
        }
        public async Task PostImageS3Reqesi(ProductApi.Row itemImg, int index,  string ProductId)
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
                _db.Images.Add(new Image()
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
