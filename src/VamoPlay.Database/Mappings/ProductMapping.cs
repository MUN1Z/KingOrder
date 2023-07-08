using VamoPlay.Database.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Database.Mappings
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public override void Map(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Guid);

            builder.Property(e => e.Gtin)
               .HasColumnType("varchar(13)")
               .IsRequired();

            builder.Property(e => e.Name)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.Price)
               .HasColumnType("decimal")
               .IsRequired();

            builder.Property(e => e.Discount)
               .HasColumnType("decimal")
               .IsRequired();

            builder.Property(e => e.Thumb)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.BarCode)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.Favorite)
               .HasColumnType("bit")
               .IsRequired();

            builder.ToTable("Product");
        }
    }
}
