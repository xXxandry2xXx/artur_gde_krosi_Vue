namespace artur_gde_krosi_Vue.Server.Models.BdModel
{
    public class ModelKrosovock
    {
        public string ModelKrosovockId { get; set; }
        public string name { get; set; }

        public List<Product> Products { get; set; }

        public string BrendID { get; set; }
        public Brend Brend { get; set; }

    }
}
