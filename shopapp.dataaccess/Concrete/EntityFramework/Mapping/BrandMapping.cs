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
            new Brand { Id = 3, Name = "Lenova", Url = "lenova", SubCategoryId = 2 },
            new Brand { Id = 4, Name = "Asus", Url = "asus", SubCategoryId = 2 },
			new Brand { Id = 5, Name = "Casper", Url = "casper", SubCategoryId = 3 },
			new Brand { Id = 6, Name = "Lenova", Url = "lenova", SubCategoryId = 3 },
			new Brand { Id = 7, Name = "Lenova", Url = "lenova", SubCategoryId = 4 },
			new Brand { Id = 8, Name = "MSI", Url = "msi", SubCategoryId = 4 },
			new Brand { Id = 9, Name = "Dell", Url = "dell", SubCategoryId = 5 },
			new Brand { Id = 10, Name = "Samsung", Url = "samsung", SubCategoryId = 5 },
			new Brand { Id = 11, Name = "Samsung", Url = "samsung", SubCategoryId = 6},
            new Brand { Id = 12, Name = "Apple", Url = "apple", SubCategoryId = 6},
			new Brand { Id = 13, Name = "Samsung", Url = "samsung", SubCategoryId = 7 },
			new Brand { Id = 14, Name = "Apple", Url = "apple", SubCategoryId = 7 },
			new Brand { Id = 15, Name = "Samsung", Url = "samsung", SubCategoryId = 8 },
			new Brand { Id = 16, Name = "Apple", Url = "apple", SubCategoryId = 8 },
			new Brand { Id = 17, Name = "Apple", Url = "apple", SubCategoryId = 9},
            new Brand { Id = 18, Name = "Lenova", Url = "lenova", SubCategoryId = 9},
            new Brand { Id = 19, Name = "Vestel", Url = "vestel", SubCategoryId = 10},
            new Brand { Id = 20, Name = "Samsung", Url = "samsung", SubCategoryId = 10},
			new Brand { Id = 21, Name = "Vestel", Url = "vestel", SubCategoryId = 11 },
			new Brand { Id = 22, Name = "Samsung", Url = "samsung", SubCategoryId = 11 },
			new Brand { Id = 23, Name = "Vestel", Url = "vestel", SubCategoryId = 12 },
			new Brand { Id = 24, Name = "Samsung", Url = "samsung", SubCategoryId = 12 },
			new Brand { Id = 25, Name = "Canon", Url = "canon", SubCategoryId = 13 },
			new Brand { Id = 26, Name = "Nikon", Url = "nikon", SubCategoryId = 13 },
			new Brand { Id = 27, Name = "Canon", Url = "canon", SubCategoryId = 14 },
			new Brand { Id = 28, Name = "Nikon", Url = "nikon", SubCategoryId = 14 },
			new Brand { Id = 29, Name = "Canon", Url = "canon", SubCategoryId = 15 },
			new Brand { Id = 30, Name = "Nikon", Url = "nikon", SubCategoryId = 15 },
			new Brand { Id = 31, Name = "Autel", Url = "autel", SubCategoryId = 16 },
			new Brand { Id = 32, Name = "Samsung", Url = "samsung", SubCategoryId = 17 },
			new Brand { Id = 33, Name = "Samsung", Url = "samsung", SubCategoryId = 18 },
			new Brand { Id = 34, Name = "Apple", Url = "apple", SubCategoryId = 19 }


			);
    }
}
