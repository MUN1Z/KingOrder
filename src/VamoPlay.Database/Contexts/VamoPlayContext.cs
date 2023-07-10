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
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<TournamentCategory> TournamentCategory { get; set; }

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
            modelBuilder.AddConfiguration(new UserMapping());
            modelBuilder.AddConfiguration(new UserRoleMapping());
            modelBuilder.AddConfiguration(new TournamentMapping());
            modelBuilder.AddConfiguration(new TournamentCategoryMapping());

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
