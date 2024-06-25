using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ContelerViews.FilterViews;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService
{
    public class FilterService
    {
        ApplicationIdentityContext db;

        public FilterService(ApplicationIdentityContext context)
        {
            db = context;
        }

        public async Task<List<Brend>> GetBrends()
        {
            return db.Brends.AsNoTracking().ToList();
        }
        public async Task<List<ModelKrosovoksView>> GetModelKrosovocks( List<string> brendsIds )
        {
            List<ModelKrosovoksView> modelKrosovocks = db.Brends.Where(x => (brendsIds == null || brendsIds.Count == 0) || brendsIds.Any(y => y == x.BrendId))
                .Include(x => x.ModelKrosovocks)
                .AsNoTracking()
                .Select(x => MapModelKrosovocks(x)).ToList();
            return modelKrosovocks;
        }
        private static ModelKrosovoksView MapModelKrosovocks(Brend brend)
        {
            ModelKrosovoksView modelKrosovoksView = new ModelKrosovoksView(brend.name,brend.ModelKrosovocks.Select(x=>MapModelKrosovock(x)).ToList());
            return modelKrosovoksView;
        }
        private static ModelKrosovoksView.ModelKrosovok MapModelKrosovock(ModelKrosovock modelKrosovock)
        {
            ModelKrosovoksView.ModelKrosovok modelKrosovok = new ModelKrosovoksView.ModelKrosovok(modelKrosovock.name,modelKrosovock.ModelKrosovockId);
            return modelKrosovok;
        }
        public async Task<List<double>> GetShoeSizes()
        {
            List<double> shoeSizes = db.Variants.AsNoTracking().Select(x => x.shoeSize).Distinct().ToList();
            return shoeSizes;
        }
        public async Task<MinMaxPriseView> MinMaxPrise()
        {
            Variant? variantMin = db.Variants.AsNoTracking().OrderBy(x => x.prise).FirstOrDefault();
            Variant? variantMax = db.Variants.AsNoTracking().OrderByDescending(x => x.prise).FirstOrDefault();
            MinMaxPriseView? minMaxPriseView = new MinMaxPriseView(variantMin is not null ? variantMin.prise : 0, variantMax is not null ? variantMax.prise : 0);
            return minMaxPriseView;
        }
    }
}
