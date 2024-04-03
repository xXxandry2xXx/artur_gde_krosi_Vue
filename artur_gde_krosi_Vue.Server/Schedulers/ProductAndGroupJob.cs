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
        ProductApi productApi = new ProductApi();
        getApiRequest<ProductApi.Root> requestProduct = new getApiRequest<ProductApi.Root>();
        productApi.root = await requestProduct.GetApiReqesi(productApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/product?expand=images.miniature.href,productFolder");

        GroupApi groupApi = new GroupApi();
        getApiRequest<GroupApi.Root> requestGroup = new getApiRequest<GroupApi.Root>();
        groupApi.root = await requestGroup.GetApiReqesi(groupApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/productfolder?expand=productFolder");

        VariantApi variantApi = new VariantApi();
        getApiRequest<VariantApi.Root> requestVariant = new getApiRequest<VariantApi.Root>();
        variantApi.root = await requestVariant.GetApiReqesi(variantApi.root, "https://api.moysklad.ru/api/remap/1.2/entity/variant?expand=rows.images,product");

        StockApi stockApi = new StockApi();
        getApiRequest<StockApi.Root> requestVariantStok = new getApiRequest<StockApi.Root>();
        stockApi.root = await requestVariantStok.GetApiReqesi(stockApi.root, "https://api.moysklad.ru/api/remap/1.2/report/stock/all", false);

        using (var scope = _provider.CreateScope())
        {
            var serviceProviderWithLogger = new ServiceCollection()
            .AddLogging(builder => builder.AddConsole())
            .BuildServiceProvider();
            var loggerFactory = serviceProviderWithLogger.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddProvider(new LoggerProvider());
            Console.WriteLine("после");

            IGroupParserService _groupParserService = scope.ServiceProvider.GetRequiredService<IGroupParserService>();
            try
            {
                List<Brend> brends = await _groupParserService.BrendsPars(groupApi);
                List<ModelKrosovock> modelKrosovocks = await _groupParserService.ModelKrosovoksPars(groupApi, brends);

                IProductParserService _productParserService = scope.ServiceProvider.GetRequiredService<IProductParserService>();
                List<Product> products = await _productParserService.ProductPars(productApi, modelKrosovocks);
                await _productParserService.VariantPars(variantApi, stockApi, products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}



