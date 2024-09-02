using artur_gde_krosi_Vue.Server.Contracts;
using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Services.ControlerService.CharacteristicProductServices
{
    public class CharacteristicProductsService
    {
        private readonly IRepositoryManager _repository;

        public CharacteristicProductsService(IRepositoryManager repository)
        {
            _repository = repository;
        }
        public string AddCharacteristicProducts(string ProductId, string name)
        {
            _repository.CharacteristicProducts.CreateCharac(new CharacteristicProduct
            {
                name = name, 
                ProductId = ProductId
            });
            _repository.Save();
            return _repository.CharacteristicProducts.GetCharacteristicProductId(true,name,ProductId);

        }
        public async Task EditCharacteristicProducts(string CharacteristicProductId, string name)
        {
            CharacteristicProduct characteristic = _repository.CharacteristicProducts.GetCharacteristicProduct(true, CharacteristicProductId);
            if (characteristic == null) throw new ArgumentException("характеристика не найдена");
            characteristic.name = name;
            _repository.Save();
        }
        public async Task DeleteCharacteristicProducts(string CharacteristicProductId)
        {
            _repository.CharacteristicProducts.RemoveCharac(true ,CharacteristicProductId);
            _repository.Save();
        }

    }
}
