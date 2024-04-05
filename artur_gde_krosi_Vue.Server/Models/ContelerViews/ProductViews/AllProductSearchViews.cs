namespace artur_gde_krosi_Vue.Server.Models.ContelerViews.ProductVies
{
    public class AllProductSearchViews
    {
        public AllProductSearchViews(string productId, string name, string herfImage)
        {
            ProductId = productId;
            this.name = name;
            this.herfImage = herfImage;
        }

        public string ProductId { get; set; }
        public string name { get; set; }
        public string herfImage { get; set; }
    }
}
