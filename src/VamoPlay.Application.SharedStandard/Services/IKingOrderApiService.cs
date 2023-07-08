using KingOrder.Application.Shared.ViewModels.Response;
using Refit;

namespace KingOrder.Application.Shared.Services
{
    public interface IKingOrderApiService
    {
        [Get("/product/")]
        Task<IEnumerable<ProductResponseViewModel>> GetProductsAsync();
    }
}
