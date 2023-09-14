using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class MainCategoryMapping : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
			builder.HasData(
                new MainCategory { Id = 1, Name = "Computer", Url = "computer" },
                new MainCategory { Id = 2, Name = "Phone", Url = "phone" },
                new MainCategory { Id = 3, Name = "TV", Url = "tv" },
                new MainCategory { Id = 4, Name = "Camera", Url = "camera" },
                new MainCategory { Id = 5, Name = "Accessory", Url = "accessory" }

                );
        }
    }
}
