namespace artur_gde_krosi_Vue.Server.Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ICharacteristicProductsRepository CharacteristicProducts { get; }
        ICharacteristicProductsValueRepository CharacteristicProductsValue { get; }
        void Save();
    }
}
