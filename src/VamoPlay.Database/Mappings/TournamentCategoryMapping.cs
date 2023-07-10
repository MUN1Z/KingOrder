using VamoPlay.Database.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Database.Mappings
{
    public class TournamentCategoryMapping : EntityTypeConfiguration<TournamentCategory>
    {
        public override void Map(EntityTypeBuilder<TournamentCategory> builder)
        {
            builder.HasKey(c => c.Guid);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnType("text")
               .IsRequired();

            builder.ToTable("TournamentCategory");
        }
    }
}
