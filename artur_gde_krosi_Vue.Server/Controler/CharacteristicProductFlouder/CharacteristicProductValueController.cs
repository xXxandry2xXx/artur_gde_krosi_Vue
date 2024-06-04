using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Controller.CharacteristicProductFolder
{
    //[Authorize(Roles = "Manager")]
    [Route("api/CharacteristicProductFolder/[controller]")]
    [ApiController]
    public class CharacteristicProductValueController : ControllerBase
    {
        private readonly ApplicationIdentityContext db;
        private readonly CharacteristicProductValueService _characteristicProductValueService;

        public CharacteristicProductValueController(ApplicationIdentityContext context, CharacteristicProductValueService characteristicProductValueService)
        {
            db = context;
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
            await _characteristicProductValueService.EditCharacteristicProductsValue(value, CharacteristicProductValueId);
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
