namespace artur_gde_krosi_Vue.Server.Models.ContelerViews
{
    public class ShoppingCartView
    {
        public ShoppingCartView(string shoppingСartId, int quantity, bool availability)
        {
            ShoppingСartId = shoppingСartId;
            this.quantity = quantity;
            this.availability = availability;
        }
        public string ShoppingСartId {  get; set; }
        public int quantity { get; set; }
        public bool availability { get; set; }
    }
}
