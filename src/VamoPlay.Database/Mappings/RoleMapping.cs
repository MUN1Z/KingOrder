using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VamoPlay.CrossCutting.Auth.Constants;
using VamoPlay.Database.Extensions;
using VamoPlay.Domain.Entities;
using VamoPlay.Domain.Enums;
using VamoPLay.Infra.Database.Comparers;
using VamoPLay.Infra.Database.Converters;

namespace VamoPlay.Database.Mappings
{
    public class RoleMapping : EntityTypeConfiguration<Role>
    {
        public override void Map(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(c => c.Guid);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Description)
               .HasColumnType("varchar(1000)");

            var converter = new EnumCollectionJsonValueConverter<ClaimType>();
            var comparer = new CollectionValueComparer<ClaimType>();

            builder.Property(e => e.UserPermissions)
                .HasConversion(converter)
                .Metadata.SetValueComparer(comparer);

            builder.HasQueryFilter(r => r.Name != AuthenticationConstants.SuperAdministratorRoleName);

            builder.ToTable("Role");
        }
    }
}
