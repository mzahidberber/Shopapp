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
            new Product { Id = 1, Name = "Dell Laptop", Url = "dell-laptop",MainCategoryId=1,CategoryId=1,SubCategoryId=1,BrandId=1,Stock=10,Price = 10, Description = "Güzel laptop", HomeImageUrl = "lenovaimg1.jpg", IsApprove = true, IsHome = true },
            new Product { Id = 2, Name = "Lenova Tablet", Url = "lenova-tablet", MainCategoryId = 2, CategoryId = 6, SubCategoryId = 9, BrandId = 17,Stock=11, Price = 101, Description = "Güzel tablet", HomeImageUrl = "2.jpg", IsApprove = true, IsHome = true }
            );
    }
}
