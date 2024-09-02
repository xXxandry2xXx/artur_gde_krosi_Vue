using artur_gde_krosi_Vue.Server.Contracts.Repositories;
using artur_gde_krosi_Vue.Server.Models.ProjecktSetings;

namespace artur_gde_krosi_Vue.Server.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private ApplicationIdentityContext db;
        private ICharacteristicProductsRepository _characteristicProductsRepository;
        private ICharacteristicProductsValueRepository _characteristicProductsValueRepository;

        public RepositoryManager(ApplicationIdentityContext db)
        {
            this.db = db;
        }

        public ICharacteristicProductsRepository CharacteristicProducts
        {
            get
            {
                if (_characteristicProductsRepository == null)
                    _characteristicProductsRepository = new CharacteristicProductsRepository(db);
                return _characteristicProductsRepository;
            }
        }
        public ICharacteristicProductsValueRepository CharacteristicProductsValue
        {
            get
            {
                if (_characteristicProductsValueRepository == null)
                    _characteristicProductsValueRepository = new CharacteristicProductsValueRepository(db);
                return _characteristicProductsValueRepository;
            }
        }


        public void Save() => db.SaveChanges();
    }
}
