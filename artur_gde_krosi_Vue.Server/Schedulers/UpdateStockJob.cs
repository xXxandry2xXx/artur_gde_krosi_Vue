using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Quartz;

namespace artur_gde_krosi_Vue.Server.Schedulers
{
    public class UpdateStockJob : IJob
    {
        private readonly IServiceProvider _provider;

        public UpdateStockJob(IServiceProvider provider)
        {
            _provider = provider;
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
            StockApi stockApi = new StockApi();
            getApiRequest<StockApi.Root> requestVariant = new getApiRequest<StockApi.Root>();
            stockApi.root = await requestVariant.GetApiReqesi(stockApi.root, "https://api.moysklad.ru/api/remap/1.2/report/stock/all",false);

            using (var scope = _provider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationIdentityContext>();

                var serviceProviderWithLogger = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole())
                    .BuildServiceProvider();

                var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();

                loggerFactory.AddProvider(new LoggerProvider());

                List<Variant> variants = dbContext.Variants.ToList();
                foreach (var item in stockApi.root.rows)
                {
                    if (variants.Any(x=> x.externalCode == item.externalCode))
                    {
                        if (item.stock.Contains("."))
                        {
                            item.stock = item.stock.Split('.')[0];
                        }
                        try
                        {
                            dbContext.Variants.FirstOrDefault(x => x.externalCode == item.externalCode).quantityInStock = Convert.ToInt32(item.stock);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex+"");
                        }
                    } 
                }
                Console.WriteLine("после");

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }

}
