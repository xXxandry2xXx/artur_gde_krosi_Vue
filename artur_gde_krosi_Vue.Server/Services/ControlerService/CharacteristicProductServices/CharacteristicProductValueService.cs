using Amazon.Runtime.Internal.Util;
using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static artur_gde_krosi_Vue.Server.Models.moyskladApi.VariantApi;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices
{
    public class CharacteristicProductValueService
    {
        private readonly IRepositoryManager _repository;

        public CharacteristicProductValueService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public string AddCharacteristicProductsValue(string value, string CharacteristicProductId)
        {
            _repository.CharacteristicProductsValue.CreateCharacVal(new CharacteristicProductValue
            {
                Value = value,
                CharacteristicProductId = CharacteristicProductId
            });
            _repository.Save();            
            return _repository.CharacteristicProductsValue.GetCharacteristicProductId(true, value, CharacteristicProductId);
        }
        public async Task EditCharacteristicProductsValue(string CharacteristicProductValueId, string value)
        {
            CharacteristicProductValue characteristicProductValue = _repository.CharacteristicProductsValue.GetCharacteristicProduct(true, CharacteristicProductValueId);
            if (characteristicProductValue == null) throw new ArgumentException("значение характеристики не найдено");
            characteristicProductValue.Value = value;
            _repository.Save();
        }
        public async Task DeleteCharacteristicProductsValue(string CharacteristicProductValueId)
        {
            _repository.CharacteristicProductsValue.RemoveCharac(true, CharacteristicProductValueId);
            _repository.Save();
        }
    }
}
