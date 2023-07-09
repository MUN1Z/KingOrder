using VamoPlay.Database.Extensions;
using VamoPlay.Database.Mappings;
using VamoPlay.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VamoPlay.CrossCutting.Auth.Entities;

namespace VamoPlay.Database.Contexts
{
    public class VamoPlayContext : IdentityDbContext<UserIdentity, IdentityRole, string>
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<User> UserAccount { get; set; }

        public VamoPlayContext(DbContextOptions<VamoPlayContext> options) : base(options)
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
            modelBuilder.AddConfiguration(new UserMapping());

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
