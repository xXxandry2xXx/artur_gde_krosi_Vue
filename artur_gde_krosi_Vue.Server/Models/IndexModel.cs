using artur_gde_krosi_Vue.Server.Models.BdModel;

namespace artur_gde_krosi_Vue.Server.Models
{
    public class IndexModel
    {
        public List<Product> products { get; set; }
        public List<Brend> brends { get; set; }
        public int ProductCount { get; set; }
        public SortState SortState { get; set; }
        public int priseDown { get; set; }
        public int priseUp { get; set; }

        public List<double> shoeSizes { get; set; }
        public List<double> shoeSizesChecked  { get; set; }
        public List<string> brendsIds { get; set; }
        public List<string> modelKrosovocksIds { get; set; }
        public string PlaceholderContent { get; set; }
        public int pageProducts { get; set; }
        public int priseMin { get; set; }
        public int priseMax { get; set; }
    }
}
