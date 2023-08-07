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
                new Product { Id = 1, Name = "Laptop", Url = "laptop", Price = 10, Description = "Güzel laptop", HomeImageUrl = "1.jpg", Stock = 1, IsApprove = false, IsHome = false },
                new Product { Id = 2, Name = "Tablet", Url = "tablet", Price = 101, Description = "Güzel laptop", HomeImageUrl = "2.jpg", Stock = 2, IsApprove = false, IsHome = false }
                );
        }
    }
}
