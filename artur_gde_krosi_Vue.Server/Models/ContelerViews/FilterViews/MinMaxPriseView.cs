namespace artur_gde_krosi_Vue.Server.Models.ContelerViews.FilterViews
{
    public class MinMaxPriseView
    {
        public MinMaxPriseView(double priseMin, double priseMax)
        {
            this.priseMin = priseMin;
            this.priseMax = priseMax;
        }

        public double priseMin {  get; set; }
        public double priseMax {  get; set; }
    }
}
