using artur_gde_krosi_Vue.Server.Models.BdModel;

namespace artur_gde_krosi_Vue.Server.Contracts.Repositories
{
    public interface ICharacteristicProductsValueRepository
    {
        void CreateCharacVal(CharacteristicProductValue characteristicProductValue);
        string GetCharacteristicProductId(bool trackChanges, string value, string CharacteristicProductId);
        CharacteristicProductValue GetCharacteristicProduct(bool trackChanges, string _CharacteristicProductValueId);
        void RemoveCharac(bool trackChanges, string CharacteristicProductValueId);
    }
}
