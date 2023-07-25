﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;
using System.Reflection.Emit;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(c=>new {c.CategoryId,c.ProductId});
        }
    }
}
