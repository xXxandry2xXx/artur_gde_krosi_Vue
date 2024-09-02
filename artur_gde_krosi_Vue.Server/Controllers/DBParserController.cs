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

namespace artur_gde_krosi_Vue.Server.Controller
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/")]
    [ApiController]
    public class DBParserController : ControllerBase
    {
        private readonly ILogger<DBParserController> _logger;
        private readonly ISchedulerFactory factory;
        private readonly ParserService _parserService;

        public DBParserController(ILogger<DBParserController> logger, ISchedulerFactory factory, ParserService parserService)
        {
            _logger = logger;
            this.factory = factory;
            _parserService = parserService;
        }

        [HttpPost("DBParserQuantityInStock")]
        public async Task<IActionResult> ParserQuantityInStock()
        {
            if (!ParserService._semaphoreStockParser)
                throw new ArgumentException("Парсинг остатков уже выполняется");
            await _parserService.StockParserDb();
            return Ok();
        }
        [HttpPost("DBParser")]
        public async Task<IActionResult> Parser()
        {
            if (!ParserService._semaphoreAllParser)
                throw new ArgumentException("Парсинг всего апи уже выполняется");
            await _parserService.AllParserDb();
            return Ok();
        }
    }
}

