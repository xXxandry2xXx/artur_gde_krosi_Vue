using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Repositories
{
    public class CharacteristicProductsValueRepository : RepositoryBase<CharacteristicProductValue>, ICharacteristicProductsValueRepository
    {
        public CharacteristicProductsValueRepository(ApplicationIdentityContext _applicationIdentityContext) : base(_applicationIdentityContext)
        {
        }
        public void CreateCharacVal(CharacteristicProductValue characteristicProductValue) => Create(characteristicProductValue);

        public string GetCharacteristicProductId(bool trackChanges, string value, string CharacteristicProductId) =>
            FindAll(trackChanges).Where(x => x.Value == value && x.CharacteristicProductId == CharacteristicProductId).FirstOrDefault().CharacteristicProductId;

        public CharacteristicProductValue GetCharacteristicProduct(bool trackChanges, string _CharacteristicProductValueId) =>
            FindByCondition(x => x.CharacteristicProductValueId == _CharacteristicProductValueId, true).FirstOrDefault();

        public void RemoveCharac(bool trackChanges, string CharacteristicProductValueId) =>
            FindByCondition(x => x.CharacteristicProductValueId == CharacteristicProductValueId, true).ExecuteDeleteAsync();
    }
}
