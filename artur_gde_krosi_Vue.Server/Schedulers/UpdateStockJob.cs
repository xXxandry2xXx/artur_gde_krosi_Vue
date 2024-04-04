using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.Parser;
using Quartz;

namespace artur_gde_krosi_Vue.Server.Schedulers
{
    public class UpdateStockJob : IJob
    {
        private readonly IServiceProvider _provider;

        public UpdateStockJob(IServiceProvider providere)
        {
            _provider = providere;
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
            using (var scope = _provider.CreateScope())
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

                StockApi stockApi = new StockApi();
                getApiRequest<StockApi.Root> requestVariant = new getApiRequest<StockApi.Root>();
                stockApi.root = await requestVariant.GetApiReqesi(stockApi.root, "https://api.moysklad.ru/api/remap/1.2/report/stock/all", configuration, false);


                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityContext>();
                var serviceProviderWithLogger = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole())
                    .BuildServiceProvider();
                var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();
                loggerFactory.AddProvider(new LoggerProvider());

                IProductParserService _productParserService = scope.ServiceProvider.GetRequiredService<IProductParserService>();
                var _db = scope.ServiceProvider.GetRequiredService<ApplicationIdentityContext>();
                await _productParserService.QuantityInStockPars(_db, stockApi);
                _db.SaveChanges();
            }
        }
    }

}
