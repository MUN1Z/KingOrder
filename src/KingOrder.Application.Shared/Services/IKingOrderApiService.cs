using KingOrder.Application.Shared.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KingOrder.Application.Shared.Services
{
    public interface IKingOrderApiService
    {
        Task<IEnumerable<ProductResponseViewModel>> GetProductsAsync();
        Task<ProductResponseViewModel> GetProductByGuidAsync(Guid guid);
        Task<ProductResponseViewModel> GetProductByGuidAsync(string guid);
        Task<ProductResponseViewModel> CreateProduct(ProductRequestViewModel product);

        Task<ProductResponseViewModel> FavoriteProduct(Guid guid);

        Task<ProductResponseViewModel> UpdateProduct(Guid guid, ProductRequestViewModel product);

        Task<bool> DeleteProduct(Guid guid);
    }
}
