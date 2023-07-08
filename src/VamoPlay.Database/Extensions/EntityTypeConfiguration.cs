using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VamoPlay.Database.Extensions
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
