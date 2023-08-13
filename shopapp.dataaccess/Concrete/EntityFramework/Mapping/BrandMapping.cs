using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class BrandMapping : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasOne(x=>x.SubCategory).WithMany(x=>x.Brands).HasForeignKey(x=>x.SubCategoryId);
        builder.HasData(
            new Brand { Id = 1, Name = "Dell", Url = "dell", SubCategoryId = 1 },
            new Brand { Id = 2, Name = "Asus", Url = "asus", SubCategoryId = 1 },
            new Brand { Id = 3, Name = "Dell", Url = "dell", SubCategoryId = 2 },
            new Brand { Id = 4, Name = "Asus", Url = "asus", SubCategoryId = 2 },
			new Brand { Id = 5, Name = "Dell", Url = "dell", SubCategoryId = 3 },
			new Brand { Id = 6, Name = "Asus", Url = "asus", SubCategoryId = 3 },
			new Brand { Id = 7, Name = "Dell", Url = "dell", SubCategoryId = 4 },
			new Brand { Id = 8, Name = "Asus", Url = "asus", SubCategoryId = 4 },
			new Brand { Id = 9, Name = "Samsung", Url = "samsung", SubCategoryId = 5},
            new Brand { Id = 10, Name = "Apple", Url = "apple", SubCategoryId = 5},
            new Brand { Id = 11, Name = "Apple", Url = "apple", SubCategoryId = 6},
            new Brand { Id = 12, Name = "Lenova", Url = "lenova", SubCategoryId = 6},
            new Brand { Id = 13, Name = "Vestel", Url = "vestel", SubCategoryId = 7},
            new Brand { Id = 14, Name = "Samsung", Url = "samsung", SubCategoryId = 7}

            );
    }
}
