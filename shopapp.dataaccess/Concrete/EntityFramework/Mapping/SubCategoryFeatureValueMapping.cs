using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class SubCategoryFeatureValueMapping : IEntityTypeConfiguration<SubCategoryFeatureValue>
{
    public void Configure(EntityTypeBuilder<SubCategoryFeatureValue> builder)
    {
        builder.HasOne(x=>x.SubCategoryFeature).WithMany(x=>x.SubCategoryFeatureValues).HasForeignKey(x=>x.SubCategoryFeatureId);
        builder.HasOne(x=>x.Product).WithMany(x=>x.SubCategoryFeatureValues).HasForeignKey(x=>x.ProductId);
        builder.HasData(
            new SubCategoryFeatureValue { Id = 1,ProductId = 1, Value = "GTX 3060",SubCategoryFeatureId=1 },
            new SubCategoryFeatureValue { Id = 2,ProductId = 1, Value = "Intel i7",SubCategoryFeatureId=2 },
            new SubCategoryFeatureValue { Id = 3,ProductId = 2, Value = "Intel i7",SubCategoryFeatureId=9 },
            new SubCategoryFeatureValue { Id = 4,ProductId = 2, Value = "GT 360",SubCategoryFeatureId=10 }

            );
    }
}
