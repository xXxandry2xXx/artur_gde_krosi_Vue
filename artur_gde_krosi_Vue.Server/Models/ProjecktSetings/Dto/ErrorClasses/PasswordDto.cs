using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings.Dto.ErrorClasses
{
    public class PasswordDto : BaseDto
    {
        public IEnumerable<IdentityError> errors {  get; set; }
    }
}
