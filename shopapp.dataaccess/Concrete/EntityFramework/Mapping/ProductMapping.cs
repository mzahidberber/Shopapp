using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(x=>x.MainCategory).WithMany(x=>x.Products).HasForeignKey(x=>x.MainCategoryId);
        builder.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);
        builder.HasOne(x=>x.Brand).WithMany(x=>x.Products).HasForeignKey(x=>x.BrandId);
        builder.HasData(
            new Product { Id = 1, Name = "Dell Laptop", Url = "dell-laptop",MainCategoryId=1,CategoryId=1,SubCategoryId=1,BrandId=1,Price = 10, Description = "Güzel laptop", HomeImageUrl = "1.jpg", Stock = 1, IsApprove = false, IsHome = false },
            new Product { Id = 2, Name = "Lenova Tablet", Url = "lenova-tablet", MainCategoryId = 2, CategoryId = 4, SubCategoryId = 5, BrandId = 8, Price = 101, Description = "Güzel tablet", HomeImageUrl = "2.jpg", Stock = 2, IsApprove = false, IsHome = false }
            );
    }
}
