namespace artur_gde_krosi_Vue.Server.Models.ContelerViews.FilterViews
{
    public class ModelKrosovoksView
    {
        public ModelKrosovoksView(string name, List<ModelKrosovok> modelKrosovoks)
        {
            this.name = name;
            this.modelKrosovoks = modelKrosovoks;
        }

        public string name { get; set; }
        public List<ModelKrosovok> modelKrosovoks { get; set; }
        public class ModelKrosovok
        {
            public ModelKrosovok(string name, string modelKrosovockId)
            {
                this.name = name;
                this.modelKrosovockId = modelKrosovockId;
            }

            public string name { get; set; }
            public string modelKrosovockId { get; set; }
        }
    }
}
