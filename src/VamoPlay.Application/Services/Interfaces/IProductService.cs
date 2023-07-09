using VamoPlay.Application.Filters;
using VamoPlay.Application.ViewModels.Response;

namespace VamoPlay.Application.Services.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductResponseViewModel>> GetAll(ProductFilter filter);
        Task<ProductResponseViewModel> GetByGuid(Guid guid);
        Task<ProductResponseViewModel> Create(ProductRequestViewModel productViewModel);
        Task<ProductResponseViewModel> Favorite(Guid guid);
        Task<bool> Delete(Guid guid);
        Task<ProductResponseViewModel> Update(Guid guid, ProductRequestViewModel productViewModel);
    }
}
