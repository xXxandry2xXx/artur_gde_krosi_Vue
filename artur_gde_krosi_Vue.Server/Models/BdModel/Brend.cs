namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class Brend
    {
        public string BrendId { get; set; }
        public string name { get; set; }

        public List<ModelKrosovock> ModelKrosovocks { get; set; }
    }
}
