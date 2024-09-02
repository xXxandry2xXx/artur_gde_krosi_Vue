using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Parser
{
    public interface IAllProductParserService
    {
        Task<List<Product>> ProductPars(ApplicationIdentityContext db, ProductApi productApi, List<ModelKrosovock> modelKrosovoks);
        Task VariantPars(ApplicationIdentityContext db, VariantApi variantApi, StockApi stockApi, List<Product> products);
    }
}