using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller.CharacteristicProductFolder
{
    [Authorize(Roles = "Manager")]
    [Route("api/CharacteristicProductFolder/[controller]")]
    [ApiController]
    public class CharacteristicProductValueController : ControllerBase
    {
        private readonly CharacteristicProductValueService _characteristicProductValueService;

        public CharacteristicProductValueController( CharacteristicProductValueService characteristicProductValueService)
        {
            _characteristicProductValueService = characteristicProductValueService;
        }

        [HttpPost]
        public string AddCharacteristicProductsValue(string value, string CharacteristicProductId)
        {
            var rez = _characteristicProductValueService.AddCharacteristicProductsValue(value, CharacteristicProductId);
            return rez;
        }
        [HttpPut]
        public async Task<IActionResult> EditCharacteristicProductsValue(string CharacteristicProductValueId, string value)
        {
            await _characteristicProductValueService.EditCharacteristicProductsValue(CharacteristicProductValueId   , value);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCharacteristicProductsValue(string CharacteristicProductValueId)
        {
            await _characteristicProductValueService.DeleteCharacteristicProductsValue(CharacteristicProductValueId);
            return Ok();
        }
    }
}
