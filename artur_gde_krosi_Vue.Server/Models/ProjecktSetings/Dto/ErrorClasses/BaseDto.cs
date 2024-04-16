using System.Text.Json;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ErrorClasses
{
    public abstract class BaseDto
    {
        public int StatusCode { get; set; }

        public  override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
