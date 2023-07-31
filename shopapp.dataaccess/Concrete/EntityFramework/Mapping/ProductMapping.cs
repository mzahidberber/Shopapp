using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product { Id=1,Name="Laptop",Url="laptop",Price=10,Description="Güzel laptop",ImageUrl="1.jpg",Stock=1},
                new Product { Id=2,Name="Tablet",Url="tablet",Price=101,Description="Güzel laptop",ImageUrl="2.jpg",Stock=2}
                );
        }
    }
}
