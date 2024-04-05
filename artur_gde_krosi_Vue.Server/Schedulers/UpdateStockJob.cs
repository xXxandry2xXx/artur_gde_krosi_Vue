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

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                OnStockParser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "");
            }
        }
        public async Task OnStockParser()
        {
            using (var scope = _provider.CreateScope())
            {
                var parserService = scope.ServiceProvider.GetRequiredService<ParserService>();
                await parserService.StockParserDb();
            }
        }
    }

}
