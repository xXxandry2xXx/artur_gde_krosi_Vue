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
using artur_gde_krosi_Vue.Server.Services.Parser;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace artur_gde_krosi_Vue.Server.Controller
{
    //[Authorize(Roles = "Manager")]
    [Route("api/[controller]/")]
    [ApiController]
    public class DBParserController : ControllerBase
    {
        private readonly ILogger<DBParserController> _logger;
        private readonly ApplicationIdentityContext db;
        private readonly ISchedulerFactory factory;
        private readonly ParserService _parserService;

        public DBParserController(ILogger<DBParserController> logger, ApplicationIdentityContext context, ISchedulerFactory factory, ParserService parserService)
        {
            _logger = logger;
            db = context;
            this.factory = factory;
            _parserService = parserService;
        }

        [HttpPut("DBParserQuantityInStock")]
        public async Task<IActionResult> ParserQuantityInStock()
        {
            if (!ParserService._semaphoreAllParser.Wait(0))
                return BadRequest("Парсинг всего апи уже выполняется");
            await _parserService.AllParserDb();
            return Ok();
        }
        [HttpPut("DBParser")]
        public async Task<IActionResult> Parser()
        {
            if (!ParserService._semaphoreStockParser.Wait(0))
                return BadRequest("Парсинг остатков уже выполняется");
            await _parserService.AllParserDb();
            return Ok();
        }
    }
}

