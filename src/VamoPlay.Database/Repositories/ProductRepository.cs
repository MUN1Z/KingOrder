using VamoPlay.Database.Contexts;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace VamoPlay.Database.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(VamoPlayContext context) : base(context)
        {
        }

        public async Task<Product> GetByGtinAsync(string gtin)
            => await Db.Product.FirstOrDefaultAsync(c => c.Gtin.Equals(gtin));
    }
}
