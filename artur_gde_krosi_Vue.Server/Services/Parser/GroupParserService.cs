using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.moyskladApi;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace artur_gde_krosi_Vue.Server.Services.Parser
{
    public class GroupParserService : IGroupParserService
    {
        private readonly ApplicationIdentityContext _db;

        public GroupParserService(ApplicationIdentityContext db)
        {
            _db = db;
        }

        public async  Task<List<Brend>> BrendsPars(GroupApi groupApi)
        {
            List<Brend> brends = _db.Brends.ToList();
            foreach (var item in groupApi.root.rows)
            {
                if (item.productFolder == null)
                {
                    if (!brends.Any(x => x.BrendId == item.id))
                    {
                        _db.Brends.Add(new Brend()
                        {
                            BrendId = item.id,
                            name = item.name
                        });
                        brends.Add(new Brend()
                        {
                            BrendId = item.id,
                            name = item.name
                        });
                    }
                    else
                    {
                        Brend brend = brends.Find(x => x.BrendId == item.id);
                        if (brend.name != item.name)
                        {
                            _db.Brends.Update(new Brend()
                            {
                                BrendId = item.id,
                                name = item.name
                            });
                        }
                    }
                }
            }
            for (int i = 0; i < brends.Count; i++)
            {
                if (groupApi.root.rows.Any(x => x.id == brends[i].BrendId))
                {
                    _db.Brends.Where(x=>x.BrendId == brends[i].BrendId).ExecuteDelete();
                    brends.RemoveAt(i);
                }
            }
            return brends;
        }

        public async Task<List<ModelKrosovock>> ModelKrosovoksPars(GroupApi groupApi , List<Brend> brends)
        {
            List<ModelKrosovock> modelKrosovoks = _db.ModelKrosovocks.ToList();
            foreach (var item in groupApi.root.rows)
            {
                if (item.productFolder != null && brends.Any(x => x.BrendId == item.productFolder.id))
                {
                    if (!modelKrosovoks.Any(x => x.ModelKrosovockId == item.id))
                    {
                        _db.ModelKrosovocks.Add(new ModelKrosovock()
                        {
                            ModelKrosovockId = item.id,
                            name = item.name,
                            BrendID = item.productFolder.id
                        });
                        modelKrosovoks.Add(new ModelKrosovock()
                        {
                            ModelKrosovockId = item.id,
                        });
                    }
                    else
                    {
                        ModelKrosovock modelKrosovock = modelKrosovoks.Find(x => x.ModelKrosovockId == item.id);
                        if (modelKrosovock.name != item.name
                            && modelKrosovock.BrendID == item.productFolder.id)
                        {
                            _db.ModelKrosovocks.Update(new ModelKrosovock()
                            {
                                ModelKrosovockId = item.id,
                                name = item.name,
                                BrendID = item.productFolder.id
                            });
                        }
                    }
                }
            }
            for (int i = 0; i < modelKrosovoks.Count; i++) 
            {
                if (groupApi.root.rows.Any(x => x.id == modelKrosovoks[i].ModelKrosovockId))
                {
                    _db.ModelKrosovocks.Where(x => x.ModelKrosovockId == brends[i].BrendId).ExecuteDelete();
                    modelKrosovoks.RemoveAt(i);
                }
            }
            _db.SaveChanges();
            return modelKrosovoks;
        }
    }
}
