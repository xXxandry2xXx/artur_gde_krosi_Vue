using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public class ParserService : IParserService
    {
        private readonly DbContext db;

        public ParserService(DbContext db)
        {
            this.db = db;
        }

        public async Task Brends(GroupApi groupApi)
        {
            List<Brend> brends = new();
            foreach (var item in groupApi.root.rows)
            {
                if (item.productFolder == null)
                {
                    brends.Add(new Brend()
                    {
                        BrendId = item.id,
                        name = item.name,
                    });
                }
            }
        }
    }
}
