using KingOrder.Domain.Entities;

namespace KingOrder.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByGtinAsync(string gtin);
    }
}
