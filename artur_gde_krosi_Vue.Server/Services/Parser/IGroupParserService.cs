using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public interface IGroupParserService
    {
        Task<List<Brend>> BrendsPars(GroupApi groupApi);
        Task<List<ModelKrosovock>> ModelKrosovoksPars(GroupApi groupApi, List<Brend> brends);
    }
}