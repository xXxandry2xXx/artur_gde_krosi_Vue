using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Quartz;
using Image = artur_gde_krosi_Vue.Server.Models.BdModel.Image;
using Amazon.S3.Model;
using Amazon.S3;
using artur_gde_krosi_Vue.Server.Services.Parser;


namespace artur_gde_krosi_Vue.Server.Schedulers;

public class ProductAndGroupJob : IJob
{
    private readonly IServiceProvider _provider;


    public ProductAndGroupJob(IServiceProvider provider)
    {
        _provider = provider;

    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
           await OnParser();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + "");
        }
    }
    public async Task OnParser()
    {
        using (var scope = _provider.CreateScope())
        {
            var parserService = scope.ServiceProvider.GetRequiredService<ParserService>();
            await parserService.AllParserDb();
        }
    }
}



