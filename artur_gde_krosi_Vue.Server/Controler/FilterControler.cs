using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controler
{
    [ApiController]
    public class FilterControler : ControllerBase
    {
        private readonly ILogger<ProductControler> _logger;
        ApplicationContext db;

        public FilterControler(ILogger<ProductControler> logger, ApplicationContext context)
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

        [HttpPost]
        [Route("/ModelKrosovocks")]
        public async Task<IActionResult> GetModelKrosovocks([FromForm] List<string> brendsIds = null)
        {
            var modelKrosovocks = db.Brends.Where(x => (brendsIds == null || brendsIds.Count == 0) || brendsIds.Any(y => y == x.BrendId))
                .Include(x => x.ModelKrosovocks)
                .Select(x => new
                {
                    Name = x.name,
                    ModelKrosovocks = x.ModelKrosovocks.Select(y => new {
                        name = y.name,
                        modelKrosovockId = y.ModelKrosovockId
                    })
                }).ToList();

            return Ok(modelKrosovocks);
        }
        [HttpGet]
        [Route("/ShoeSizes")]
        public async Task<IActionResult> GetShoeSizes()
        {
            List<double> shoeSizes = db.Variants.Select(x => x.shoeSize).Distinct().ToList();
            return Ok(shoeSizes);
        }
    }
}
