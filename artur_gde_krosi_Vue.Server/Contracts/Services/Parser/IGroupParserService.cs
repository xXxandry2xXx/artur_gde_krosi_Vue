using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Parser
{
    public interface IGroupParserService
    {
        Task<List<Brend>> BrendsPars(ApplicationIdentityContext db, GroupApi groupApi);
        Task<List<ModelKrosovock>> ModelKrosovoksPars(ApplicationIdentityContext db, GroupApi groupApi, List<Brend> brends);
    }
}