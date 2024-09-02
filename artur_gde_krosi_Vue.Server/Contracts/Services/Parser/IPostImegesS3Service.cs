using artur_gde_krosi_Vue.Server.Models;

namespace artur_gde_krosi_Vue.Server.Contracts.Services.Parser
{
    public interface IPostImegesS3Service
    {
        Task PostImageS3Reqesi(ProductApi.Row itemImg, int index, string ProductId);
    }
}