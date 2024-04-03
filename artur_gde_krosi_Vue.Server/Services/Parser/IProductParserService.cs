using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public interface IProductParserService
    {
        Task<List<Product>> ProductPars(ProductApi productApi, List<ModelKrosovock> modelKrosovoks);
        Task VariantPars(VariantApi variantApi, StockApi stockApi, List<Product> products);
        Task QuantityInStockPars(StockApi stockApi);
    }
}