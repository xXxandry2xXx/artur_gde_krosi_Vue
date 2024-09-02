using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.BdModel;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Repositories
{
    public class CharacteristicProductsRepository : RepositoryBase<CharacteristicProduct>, ICharacteristicProductsRepository
    {
        public CharacteristicProductsRepository(ApplicationIdentityContext _applicationIdentityContext) : base(_applicationIdentityContext)
        {
        }
        public void CreateCharac(CharacteristicProduct characteristicProduct) => Create(characteristicProduct);

        public string GetCharacteristicProductId(bool trackChanges,string name, string ProductId) =>
            FindAll(trackChanges).Where(x => x.name == name && x.ProductId == ProductId).FirstOrDefault().CharacteristicProductId;

        public CharacteristicProduct GetCharacteristicProduct(bool trackChanges, string CharacteristicProductId) =>
            FindByCondition(x => x.CharacteristicProductId == CharacteristicProductId, true).FirstOrDefault();

        public void RemoveCharac(bool trackChanges, string CharacteristicProductId) =>
            FindByCondition(x => x.CharacteristicProductId == CharacteristicProductId, true).ExecuteDeleteAsync();
    }
}
