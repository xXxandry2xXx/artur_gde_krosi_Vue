using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class CharacteristicProductValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CharacteristicProductValueId { get; set; }
        public string Value { get; set; }

        public string CharacteristicProductId { get; set; }
        public CharacteristicProduct CharacteristicProduct { get; set; }
    }
}
