namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class ImageVariant
    {
        public string ImageVariantId { get; set; }
        public string name { get; set; }
        public byte[] ImageData { get; set; }


        public string VariantId { get; set; }
        public Variant Variant { get; set; }
    }
}
