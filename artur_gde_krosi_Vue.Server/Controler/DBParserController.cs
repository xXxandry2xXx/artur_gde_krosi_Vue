using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Quartz;
using artur_gde_krosi_Vue.Server.Schedulers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controller
{
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]/")]
    [ApiController]
    public class DBParserController : ControllerBase
    {
        private readonly ILogger<DBParserController> _logger;
        ApplicationIdentityContext db;
        private readonly ISchedulerFactory factory;


        public DBParserController(ILogger<DBParserController> logger, ApplicationIdentityContext context, ISchedulerFactory factory)
        {
            _logger = logger;
            db = context;
            this.factory = factory;
        }

        [HttpPut("DBParserQuantityInStock")]
        public async Task<IActionResult> ParserQuantityInStock()
        {
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.TriggerJob(new JobKey("artur_gde_krosi_Vue.Server.Schedulers.UpdateStockJob"));
            return Ok();
        }
        [HttpPut("DBParser")]
        public async Task<IActionResult> Parser()
        {
            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.TriggerJob(new JobKey("artur_gde_krosi_Vue.Server.Schedulers.ProductAndGroupJob"));

            return Ok();
        }
    }
}

