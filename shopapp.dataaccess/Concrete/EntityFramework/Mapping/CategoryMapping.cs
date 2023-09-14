using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne(x=>x.MainCategory).WithMany(x=>x.Categories).HasForeignKey(x=>x.MainCategoryId);
			builder.HasData(
                new Category { Id = 1, Name = "Notebook", Url = "notebook",MainCategoryId=1 },
                new Category { Id = 2, Name = "Desktop Computer", Url = "desktoppc",MainCategoryId=1 },
                new Category { Id = 3, Name = "Game Computer", Url = "desktop-game-pc",MainCategoryId=1 },
                new Category { Id = 4, Name = "Mobile Phone", Url = "mobilephone",MainCategoryId=2 },
                new Category { Id = 5, Name = "Push Button Phone", Url = "pushbuttonphone",MainCategoryId=2 },
                new Category { Id = 6, Name = "Tablet", Url = "tablet",MainCategoryId=2 },
                new Category { Id = 7, Name = "Television", Url = "television",MainCategoryId=3},
                new Category { Id = 8, Name = "Projection", Url = "projection",MainCategoryId=3},
                new Category { Id = 9, Name = "SoundBar", Url = "soundbar",MainCategoryId=3},
                new Category { Id = 10, Name = "Camera", Url = "ccamera",MainCategoryId=4},
                new Category { Id = 11, Name = "Video Camera", Url = "videocamera",MainCategoryId=4},
                new Category { Id = 12, Name = "Drone", Url = "drone",MainCategoryId=4},
                new Category { Id = 13, Name = "Earphones", Url = "earphones", MainCategoryId=5},
                new Category { Id = 14, Name = "Phone case", Url = "phonecase", MainCategoryId=5}

                );
        }
    }
}
