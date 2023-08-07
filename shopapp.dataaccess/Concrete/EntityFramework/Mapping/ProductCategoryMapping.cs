using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class ProductCategoryMapping : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(c => new { c.CategoryId, c.ProductId });
            builder.HasData(
                new ProductCategory { CategoryId = 1, ProductId = 1 },
                new ProductCategory { CategoryId = 2, ProductId = 2 }
            );
        }
    }
}
