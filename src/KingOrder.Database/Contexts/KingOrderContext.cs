using KingOrder.Database.Extensions;
using KingOrder.Database.Mappings;
using KingOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KingOrder.Database.Contexts
{
    public class KingOrderContext : DbContext
    {
        public DbSet<Product> Product { get; set; }

        public KingOrderContext(DbContextOptions<KingOrderContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddConfiguration(new ProductMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();
            foreach (var entry in ChangeTracker.Entries())
                if (entry.Entity is BaseEntity baseEntity)
                    if (entry.State == EntityState.Added)
                        baseEntity.CreatedAt = DateTime.UtcNow;
                    else if (entry.State == EntityState.Modified)
                        baseEntity.UpdatedAt = DateTime.UtcNow;

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
