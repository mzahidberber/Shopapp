using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class SubCategoryFeatureValueMapping : IEntityTypeConfiguration<SubCategoryFeatureValue>
{
    public void Configure(EntityTypeBuilder<SubCategoryFeatureValue> builder)
    {
        builder.HasOne(x => x.SubCategoryFeature).WithMany(x => x.SubCategoryFeatureValues).HasForeignKey(x => x.SubCategoryFeatureId);
        builder.HasOne(x => x.Product).WithMany(x => x.SubCategoryFeatureValues).HasForeignKey(x => x.ProductId);
        builder.HasData(
            new SubCategoryFeatureValue { Id = 1, ProductId = 1, Value = "GTX 3060", SubCategoryFeatureId = 1 },
            new SubCategoryFeatureValue { Id = 2, ProductId = 1, Value = "Intel i7", SubCategoryFeatureId = 2 },
            new SubCategoryFeatureValue { Id = 3, ProductId = 1, Value = "1 TB HDD", SubCategoryFeatureId = 3 },
            new SubCategoryFeatureValue { Id = 4, ProductId = 1, Value = "32 GB", SubCategoryFeatureId = 4 },
            new SubCategoryFeatureValue { Id = 5, ProductId = 1, Value = "18 Inc", SubCategoryFeatureId = 5 },
            new SubCategoryFeatureValue { Id = 6, ProductId = 2, Value = "Intel i7", SubCategoryFeatureId = 26 },
            new SubCategoryFeatureValue { Id = 7, ProductId = 2, Value = "16 GB", SubCategoryFeatureId = 27 },
            new SubCategoryFeatureValue { Id = 8, ProductId = 2, Value = "3046 mAh", SubCategoryFeatureId = 28 },
            new SubCategoryFeatureValue { Id = 9, ProductId = 2, Value = "12 MP", SubCategoryFeatureId = 29 }

            );
    }
}
