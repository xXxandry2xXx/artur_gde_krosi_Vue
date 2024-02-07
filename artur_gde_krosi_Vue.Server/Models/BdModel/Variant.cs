namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class Variant
    {
        public string VariantId { get; set; }
        public string externalCode { get; set; }
        public int quantityInStock { get; set; }
        public double shoeSize { get; set; }
        public double prise { get; set; }

        public List<ImageVariant> ImageVariant { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
