using KingOrder.Database.Contexts;
using KingOrder.Domain.Entities;
using KingOrder.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingOrder.Database.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(KingOrderContext context) : base(context)
        {
        }

        public async Task<Product> GetByGtinAsync(string gtin)
            => await Db.Product.FirstOrDefaultAsync(c => c.Gtin.Equals(gtin));
    }
}
