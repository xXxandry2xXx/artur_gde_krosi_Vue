using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public interface IProductParserService
    {
        Task<List<Product>> ProductPars(ApplicationIdentityContext _db, ProductApi productApi, List<ModelKrosovock> modelKrosovoks);
        Task VariantPars(ApplicationIdentityContext _db, VariantApi variantApi, StockApi stockApi, List<Product> products);
        Task QuantityInStockPars(ApplicationIdentityContext _db, StockApi stockApi);
    }
}