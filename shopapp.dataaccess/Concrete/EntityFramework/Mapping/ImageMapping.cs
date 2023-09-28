using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class ImageMapping : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasOne(x => x.Product).WithMany(x => x.Images).HasForeignKey(x => x.ProductId);
        builder.HasData(
                new Image { Id = 1, Url = "lenovaimg1.jpg", ProductId = 1 },
                new Image { Id = 2, Url = "lenovatablet1.jpg", ProductId = 1 },
                new Image { Id = 3, Url = "lenovaimg1.jpg", ProductId = 2 },
                new Image { Id = 4, Url = "lenovatablet1.jpg", ProductId = 2 }

                );
    }
}
