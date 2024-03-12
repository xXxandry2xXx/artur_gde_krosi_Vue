using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class CharacteristicProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CharacteristicProductId { get; set; }
        public string name { get; set; }

        public List<CharacteristicProductValue> CharacteristicProductValues { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
