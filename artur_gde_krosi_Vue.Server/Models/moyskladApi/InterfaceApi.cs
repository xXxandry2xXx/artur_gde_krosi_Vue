using System.Text.Json.Serialization;
using static artur_gde_krosi_Vue.Server.Models.ProductApi;

namespace artur_gde_krosi_Vue.Server.Models.moyskladApi
{
    public interface InterfaceApi
    {
        public Root root { get; set; }

        public  class Root
        {
            public virtual int Count()
            {
                return 0;
            }
            public virtual Root New()
            {
                return new Root();
            }
            public virtual void AddRange<T> (T root) where T : Root
            {

            }
        }
        public class Row 
        {
            
        }
    }
}
