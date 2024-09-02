using artur_gde_krosi_Vue.Server.Models.BdModel;

namespace artur_gde_krosi_Vue.Server.Contracts.Repositories
{
    public interface ICharacteristicProductsRepository
    {
        void CreateCharac(CharacteristicProduct characteristicProduct);
        string GetCharacteristicProductId(bool trackChanges, string name, string ProductId);
        CharacteristicProduct GetCharacteristicProduct(bool trackChanges, string CharacteristicProductId);
        void RemoveCharac(bool trackChanges, string CharacteristicProductId);
    }
}
