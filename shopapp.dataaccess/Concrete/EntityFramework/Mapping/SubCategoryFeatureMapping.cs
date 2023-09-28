using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class SubCategoryFeatureMapping : IEntityTypeConfiguration<SubCategoryFeature>
{
    public void Configure(EntityTypeBuilder<SubCategoryFeature> builder)
    {
        builder.HasOne(x => x.SubCategory).WithMany(x => x.SubCategoryFeatures).HasForeignKey(x => x.SubCategoryId);
        builder.HasData(
            new SubCategoryFeature { Id = 1, Name = "Display Card", SubCategoryId = 1 },
            new SubCategoryFeature { Id = 2, Name = "Processor", SubCategoryId = 1 },
            new SubCategoryFeature { Id = 3, Name = "Memory", SubCategoryId = 1 },
            new SubCategoryFeature { Id = 4, Name = "Ram", SubCategoryId = 1 },
            new SubCategoryFeature { Id = 5, Name = "Screen Size", SubCategoryId = 1 },
            new SubCategoryFeature { Id = 6, Name = "Display Card", SubCategoryId = 2 },
            new SubCategoryFeature { Id = 7, Name = "Processor", SubCategoryId = 2 },
            new SubCategoryFeature { Id = 8, Name = "Memory", SubCategoryId = 2 },
            new SubCategoryFeature { Id = 9, Name = "Ram", SubCategoryId = 2 },
            new SubCategoryFeature { Id = 10, Name = "Screen Size", SubCategoryId = 2 },
            new SubCategoryFeature { Id = 11, Name = "Display Card", SubCategoryId = 3 },
            new SubCategoryFeature { Id = 12, Name = "Processor", SubCategoryId = 3 },
            new SubCategoryFeature { Id = 13, Name = "Memory", SubCategoryId = 3 },
            new SubCategoryFeature { Id = 14, Name = "Ram", SubCategoryId = 3 },
            new SubCategoryFeature { Id = 15, Name = "Screen Size", SubCategoryId = 3 },
            new SubCategoryFeature { Id = 16, Name = "Display Card", SubCategoryId = 4 },
            new SubCategoryFeature { Id = 17, Name = "Processor", SubCategoryId = 4 },
            new SubCategoryFeature { Id = 18, Name = "Memory", SubCategoryId = 4 },
            new SubCategoryFeature { Id = 19, Name = "Ram", SubCategoryId = 4 },
            new SubCategoryFeature { Id = 20, Name = "Screen Size", SubCategoryId = 4 },
            new SubCategoryFeature { Id = 21, Name = "Display Card", SubCategoryId = 5 },
            new SubCategoryFeature { Id = 22, Name = "Processor", SubCategoryId = 5 },
            new SubCategoryFeature { Id = 23, Name = "Memory", SubCategoryId = 5 },
            new SubCategoryFeature { Id = 24, Name = "Ram", SubCategoryId = 5 },
            new SubCategoryFeature { Id = 25, Name = "Screen Size", SubCategoryId = 5 },
            new SubCategoryFeature { Id = 26, Name = "Processor", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 27, Name = "Ram", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 28, Name = "Battery", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 29, Name = "Camera", SubCategoryId = 6 },
            new SubCategoryFeature { Id = 30, Name = "Processor", SubCategoryId = 7 },
            new SubCategoryFeature { Id = 31, Name = "Ram", SubCategoryId = 7 },
            new SubCategoryFeature { Id = 32, Name = "Battery", SubCategoryId = 7 },
            new SubCategoryFeature { Id = 33, Name = "Camera", SubCategoryId = 7 },
            new SubCategoryFeature { Id = 34, Name = "Processor", SubCategoryId = 8 },
            new SubCategoryFeature { Id = 35, Name = "Ram", SubCategoryId = 8 },
            new SubCategoryFeature { Id = 36, Name = "Battery", SubCategoryId = 8 },
            new SubCategoryFeature { Id = 37, Name = "Camera", SubCategoryId = 8 },
            new SubCategoryFeature { Id = 38, Name = "Processor", SubCategoryId = 9 },
            new SubCategoryFeature { Id = 39, Name = "Ram", SubCategoryId = 9 },
            new SubCategoryFeature { Id = 40, Name = "Battery", SubCategoryId = 9 },
            new SubCategoryFeature { Id = 41, Name = "Camera", SubCategoryId = 9 },
            new SubCategoryFeature { Id = 42, Name = "Screen Size", SubCategoryId = 10 },
            new SubCategoryFeature { Id = 43, Name = "Resolution", SubCategoryId = 10 },
            new SubCategoryFeature { Id = 44, Name = "Screen Size", SubCategoryId = 11 },
            new SubCategoryFeature { Id = 45, Name = "Resolution", SubCategoryId = 11 }


            );
    }
}
