namespace artur_gde_krosi_Vue.Server.Models.ContelerViews
{
    public class ShoppingCartView
    {
        public List<ShoppingCartListElement> shoppingCartList { get; set; }
        public double totalPrise { get; set; }

        public class ShoppingCartListElement
        {
            public ShoppingCartListElement(string shoppingСartId, int quantity, bool availability, string variantId, string productId, double prise)
            {
                ShoppingСartId = shoppingСartId;
                this.quantity = quantity;
                this.availability = availability;
                VariantId = variantId;
                ProductId = productId;
                this.prise = prise;
            }
            public string ShoppingСartId { get; set; }
            public int quantity { get; set; }
            public bool availability { get; set; }
            public string VariantId { get; set; }
            public string ProductId { get; set; }
            public double prise { get; set; }
        }
    }
}
