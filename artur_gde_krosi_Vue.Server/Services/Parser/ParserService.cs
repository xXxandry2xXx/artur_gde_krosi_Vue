using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Models;
using System.Configuration;
using NuGet.Common;
using artur_gde_krosi_Vue.Server.Contracts.Services.Parser;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public class ParserService
    {
        private readonly ApplicationIdentityContext db;
        private readonly IGroupParserService _groupParserService;
        private readonly IAllProductParserService _productParserService;
        private readonly IStokProductParserService _stokProductParserService;
        private readonly IConfiguration _configuration;
        public static bool _semaphoreStockParser = true;
        public static bool _semaphoreAllParser = true;

        public ParserService(ApplicationIdentityContext db, IGroupParserService groupParserService, IAllProductParserService productParserService, IConfiguration configuration, IStokProductParserService stokProductParserService)
        {
            this.db = db;
            _groupParserService = groupParserService;
            _productParserService = productParserService;
            _configuration = configuration;
            _stokProductParserService = stokProductParserService;
        }

        public async Task AllParserDb()
        {
            try
            {
                _semaphoreAllParser = false;
                Console.WriteLine("начало парсинга апи");
                ProductApi productApi = new ProductApi();
                getApiRequest<ProductApi.Root> requestProduct = new getApiRequest<ProductApi.Root>();
                productApi.root = await requestProduct.GetApiReqesi(productApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/product?expand=images.miniature.href,productFolder", _configuration);

                GroupApi groupApi = new GroupApi();
                getApiRequest<GroupApi.Root> requestGroup = new getApiRequest<GroupApi.Root>();
                groupApi.root = await requestGroup.GetApiReqesi(groupApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/productfolder?expand=productFolder", _configuration);

                VariantApi variantApi = new VariantApi();
                getApiRequest<VariantApi.Root> requestVariant = new getApiRequest<VariantApi.Root>();
                variantApi.root = await requestVariant.GetApiReqesi(variantApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/variant?expand=rows.images,product", _configuration);

                StockApi stockApi = new StockApi();
                getApiRequest<StockApi.Root> requestVariantStok = new getApiRequest<StockApi.Root>();
                stockApi.root = await requestVariantStok.GetApiReqesi(stockApi.root, "https://api.moysklad.ru/api/remap/1.2/report/stock/all", _configuration, false);


                var serviceProviderWithLogger = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())
                .BuildServiceProvider();
                var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();
                loggerFactory.AddProvider(new LoggerProvider());

                try
                {
                    Console.WriteLine("1 - 1");
                    List<Brend> brends = await _groupParserService.BrendsPars(db, groupApi);
                    Console.WriteLine("1 - 2");
                    List<ModelKrosovock> modelKrosovocks = await _groupParserService.ModelKrosovoksPars(db, groupApi, brends);

                    Console.WriteLine("2 - 1");
                    List<Product> products = await _productParserService.ProductPars(db, productApi, modelKrosovocks);
                    Console.WriteLine("2 - 2");
                    await _productParserService.VariantPars(db, variantApi, stockApi, products);

                    Console.WriteLine("конец заполнения контекста");
                    db.SaveChanges();
                    Console.WriteLine("конец");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            finally
            {
                _semaphoreAllParser = true;
            }
        }
        public async Task StockParserDb()
        {
            try
            {
                _semaphoreStockParser = false;
                Console.WriteLine("начало парсинга остатков");
                StockApi stockApi = new StockApi();
                getApiRequest<StockApi.Root> requestVariant = new getApiRequest<StockApi.Root>();
                stockApi.root = await requestVariant.GetApiReqesi(stockApi.root, "https://api.moysklad.ru/api/remap/1.2/report/stock/all", _configuration, false);

                var serviceProviderWithLogger = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole())
                    .BuildServiceProvider();
                var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();
                loggerFactory.AddProvider(new LoggerProvider());

                await _stokProductParserService.QuantityInStockPars(db, stockApi);
                db.SaveChanges();
            }
            finally
            {
                _semaphoreStockParser = true;
            }

        }
    }
}
