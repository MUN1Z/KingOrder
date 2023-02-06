using KingOrder.Application.Shared.ViewModels.Response;
using Refit;

namespace KingOrder.Application.Shared.Services
{
    public class KingOrderApiService : IKingOrderApiService
    {
        private readonly string _url = "https://localhost:5000/api";
        private readonly IKingOrderApiService _api;

        public KingOrderApiService()
        {
            _api = RestService.For<IKingOrderApiService>(_url);
        }

        public async Task<IEnumerable<ProductResponseViewModel>> GetProductsAsync()
        {
            var products = await _api.GetProductsAsync();
            return products;
        }
    }
}