using Microsoft.AspNetCore.Identity;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ExceptionModel
{
    public class AllowedOnMailException : Exception
    {
        public AllowedOnMailException() : base() { }
        public AllowedOnMailException(string message) : base(message) { }
        public AllowedOnMailException(string message, Exception innerException) : base(message, innerException) { }
    }
}
