﻿using Microsoft.EntityFrameworkCore;

namespace KingOrder.Database.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration) where TEntity : class
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
