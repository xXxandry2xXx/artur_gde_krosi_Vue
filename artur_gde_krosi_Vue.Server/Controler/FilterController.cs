using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ILogger<FilterController> _logger;
        ApplicationIdentityContext db;

        public FilterController(ILogger<FilterController> logger, ApplicationIdentityContext context)
        {
            _logger = logger;
            db = context;
        }

        [HttpGet]
        [Route("Brends")]
        public async Task<IActionResult> GetBrends()
        {
            return Ok(db.Brends);
        }

        [HttpGet]
        [Route("ModelKrosovocks")]
        public async Task<IActionResult> GetModelKrosovocks([FromHeader] List<string> brendsIds = null)
        {
            var modelKrosovocks = db.Brends.Where(x => (brendsIds == null || brendsIds.Count == 0) || brendsIds.Any(y => y == x.BrendId))
                .Include(x => x.ModelKrosovocks)
                .Select(x => new
                {
                    Name = x.name,
                    ModelKrosovocks = x.ModelKrosovocks.Select(y => new
                    {
                        name = y.name,
                        modelKrosovockId = y.ModelKrosovockId
                    })
                }).ToList();
            return Ok(modelKrosovocks);
        }
        [HttpGet]
        [Route("ShoeSizes")]
        public async Task<IActionResult> GetShoeSizes()
        {
            List<double> shoeSizes = db.Variants.Select(x => x.shoeSize).Distinct().ToList();
            return Ok(shoeSizes);
        }
    }
}
