using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;

namespace artur_gde_krosi_Vue.Server.Controler
{
    [ApiController]
    public class ImageOnProduct : ControllerBase
    {
        private readonly ILogger<ProductControler> _logger;
        ApplicationContext db;

        public ImageOnProduct(ILogger<ProductControler> logger, ApplicationContext context)
        {
            _logger = logger;
            db = context;
        }
        [HttpGet]
        [Route("/ImageProduct")]
        public async Task<IActionResult> GetImageProduct(string id)
        {
            //return File(db.Images.FirstOrDefault(x => x.ProductId == id && x.Index == 0).ImageData, "image/jpeg");
            return Ok();
        }

        [HttpGet]
        [Route("/ImageProductS3")]
        public async Task<IActionResult> GetImageProductS3(string id)
        {
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
                    Key = "bdbe5189ed262d3cc850ee3b2f54a524.jpeg",

                };

                try
                {
                    using (var response = await client.GetObjectAsync(request))
                    using (var responseStream = response.ResponseStream)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await responseStream.CopyToAsync(memoryStream);
                            return File(memoryStream.ToArray(), "image/jpeg");

                        }
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return Ok();
        }
    }
}
