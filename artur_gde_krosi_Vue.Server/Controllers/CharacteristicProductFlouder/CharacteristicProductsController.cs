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
    public class CharacteristicProductsController : ControllerBase
    {
        private readonly ApplicationIdentityContext db;
        private readonly CharacteristicProductsService _characteristicProductsService;

        public CharacteristicProductsController(ApplicationIdentityContext context,CharacteristicProductsService characteristicProductsService)
        {
            db = context;
            _characteristicProductsService = characteristicProductsService;
        }
        [HttpPost]
        public string AddCharacteristicProducts(string ProductId, string name)
        {
            var rez = _characteristicProductsService.AddCharacteristicProducts(ProductId, name);
            return rez;
        }
        [HttpPut]
        public async Task<IActionResult> EditCharacteristicProducts(string CharacteristicProductId, string name)
        {
            await _characteristicProductsService.EditCharacteristicProducts(CharacteristicProductId, name);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCharacteristicProducts(string CharacteristicProductId)
        {
            await _characteristicProductsService.DeleteCharacteristicProducts(CharacteristicProductId);
            return Ok();
        }

    }
}
