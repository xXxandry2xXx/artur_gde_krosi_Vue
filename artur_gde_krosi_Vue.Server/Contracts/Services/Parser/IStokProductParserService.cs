using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Parser
{
    public interface IStokProductParserService
    {
        Task QuantityInStockPars(ApplicationIdentityContext db, StockApi stockApi);
    }
}
