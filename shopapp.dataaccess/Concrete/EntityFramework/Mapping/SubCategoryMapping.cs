using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class SubCategoryMapping : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasOne(x=>x.Category).WithMany(x=>x.SubCategories).HasForeignKey(x=>x.CategoryId);
			builder.HasData(
                new SubCategory { Id = 1, Name = "Laptop", Url = "laptop", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Oyun Bilgisayarı", Url = "oyunpc", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Masaüstü", Url = "masaustu", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Oyun Bilgisayarı", Url = "oyun-pc", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "Akıllı Telefon", Url = "akillitelefon", CategoryId = 3},
                new SubCategory { Id = 6, Name = "Tablet", Url = "tablet", CategoryId = 4},
                new SubCategory { Id = 7, Name = "Full HD", Url = "fullhd", CategoryId = 5}

                );
        }
    }
}
