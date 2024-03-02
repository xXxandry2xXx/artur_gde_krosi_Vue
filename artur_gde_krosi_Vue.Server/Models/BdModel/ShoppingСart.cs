using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class ShoppingСart
    {
        public string ShoppingСartId { get; set; }

        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public string VariantId { get; set; }
        public Variant Variant { get; set; }
    }
}
