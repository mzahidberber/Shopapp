using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class SubCategoryFeatureMapping : IEntityTypeConfiguration<SubCategoryFeature>
{
    public void Configure(EntityTypeBuilder<SubCategoryFeature> builder)
    {
        builder.HasOne(x=>x.SubCategory).WithMany(x=>x.SubCategoryFeatures).HasForeignKey(x=>x.SubCategoryId);
        builder.HasData(
            new SubCategoryFeature { Id = 1, Name = "Ekran Kartı",SubCategoryId=1 },
            new SubCategoryFeature { Id = 2, Name = "İşlemci",SubCategoryId=1 },
            new SubCategoryFeature { Id = 3, Name = "İşlemci",SubCategoryId=2 },
            new SubCategoryFeature { Id = 4, Name = "Ekran Kartı",SubCategoryId=2 },
			new SubCategoryFeature { Id = 5, Name = "İşlemci", SubCategoryId = 3 },
			new SubCategoryFeature { Id = 6, Name = "Ekran Kartı", SubCategoryId = 3},
			new SubCategoryFeature { Id = 7, Name = "İşlemci", SubCategoryId = 4 },
			new SubCategoryFeature { Id = 8, Name = "Ekran Kartı", SubCategoryId = 4 },
			new SubCategoryFeature { Id = 9, Name = "Kamera",SubCategoryId=5 },
            new SubCategoryFeature { Id = 10, Name = "Hafıza",SubCategoryId=5 },
            new SubCategoryFeature { Id = 11, Name = "İşlemci", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 12, Name = "Ekran Kartı", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 13, Name = "Ekran Boyutu", SubCategoryId = 7 },
            new SubCategoryFeature { Id = 14, Name = "Çözünürlük", SubCategoryId = 7 }

            );
    }
}
