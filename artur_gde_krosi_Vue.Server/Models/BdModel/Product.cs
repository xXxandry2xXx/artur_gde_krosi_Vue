namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class Product
    {
        public string ProductId { get; set; }
        public string name { get; set; }
        public double prise { get; set; }

        public List<Variant> Variants { get; set; }

        public List<Image> Images { get; set; }

        public string ModelKrosovockId { get; set; }
        public ModelKrosovock ModelKrosovock { get; set; }
    }
}
