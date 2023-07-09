using VamoPlay.Domain.Entities;

namespace VamoPlay.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByGtinAsync(string gtin);
    }
}
