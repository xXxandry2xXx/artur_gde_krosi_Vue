using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class ShoppingСart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ShoppingСartId { get; set; }
        public int quantity { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string VariantId { get; set; }
        public Variant Variant { get; set; }
    }
}
