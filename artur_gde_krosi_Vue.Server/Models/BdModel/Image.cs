using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class Image
    {

        public string ImageId { get; set; }
        public int Index { get; set; }
        public string ImageSrc { get; set; }
        [NotMapped]
        public System.Drawing.Image img { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
