namespace artur_gde_krosi_Vue.Server.Models.ContelerViews
{
    public class ShoppingCartView
    {
        public ShoppingCartView(string shoppingСartId, int quantity, bool availability, string variantId, string productId)
        {
            ShoppingСartId = shoppingСartId;
            this.quantity = quantity;
            this.availability = availability;
            VariantId = variantId;
            ProductId = productId;
        }
        public string ShoppingСartId {  get; set; }
        public int quantity { get; set; }
        public bool availability { get; set; }
        public string VariantId { get; set; }
        public string ProductId { get; set; }
    }
}
