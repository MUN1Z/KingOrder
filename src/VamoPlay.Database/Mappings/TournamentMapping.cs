using VamoPlay.Database.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Database.Mappings
{
    public class TournamentMapping : EntityTypeConfiguration<Tournament>
    {
        public override void Map(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(c => c.Guid);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.StartDate)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(e => e.EndDate)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(e => e.StartInscriptionDate)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(e => e.EndInscriptionDate)
               .HasColumnType("datetime2")
               .IsRequired();

            builder.Property(e => e.Thumb)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.Banner)
               .HasColumnType("text")
               .IsRequired();

            builder.Property(e => e.IsVisible)
               .HasColumnType("bit")
               .IsRequired();

            builder.ToTable("Tournament");
        }
    }
}
