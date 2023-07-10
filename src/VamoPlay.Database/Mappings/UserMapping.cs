﻿using VamoPlay.Database.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Database.Mappings
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Guid);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Email)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.Password)
               .HasColumnType("varchar(255)")
               .IsRequired();

            builder.Property(e => e.IsActive)
               .HasColumnType("bit")
               .IsRequired();

            builder.Property(e => e.IsDeleted)
               .HasColumnType("bit")
               .IsRequired();

            builder.Property(e => e.LastAccess)
               .HasColumnType("datetime2");

            builder.HasMany(e => e.Roles).WithMany(c => c.Users).UsingEntity(c => c.ToTable("User_Role"));

            // Globally apply the filter in all queries.
            builder.HasQueryFilter(e => !e.IsDeleted);

            builder.ToTable("User");
        }
    }
}
