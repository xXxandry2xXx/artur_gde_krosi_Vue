namespace artur_gde_krosi_Vue.Server.Models.ContelerViews.ProductVies
{
    public class ProductListView
    {
        public List<ProductList> productList { get; set; }
        public int Count { get; set; }
        public class ProductList
        {
            public ProductList(string name, string modelKrosovock, string brend_Name, int views, string productId, List<Variant> variants, string imgSrc)
            {
                this.name = name;
                ModelKrosovock = modelKrosovock;
                Brend_Name = brend_Name;
                this.views = views;
                ProductId = productId;
                Variants = variants;
                ImgSrc = imgSrc;
            }
            public string name { get; set; }
            public string ModelKrosovock { get; set; }
            public string Brend_Name { get; set; }
            public int views { get; set; }
            public string ProductId { get; set; }
            public List<Variant> Variants { get; set; }
            public string ImgSrc { get; set; }
        }
        public class Variant
        {
            public Variant(string variantId, double shoeSize, int quantityInStock, double prise)
            {
                VariantId = variantId;
                this.shoeSize = shoeSize;
                this.quantityInStock = quantityInStock;
                this.prise = prise;
            }
            public string VariantId { get; set; }
            public double shoeSize { get; set; }
            public int quantityInStock { get; set; }
            public double prise { get; set; }
        }

    }
}
