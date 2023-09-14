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
                new SubCategory { Id = 2, Name = "Game Notebook", Url = "gamenotebook", CategoryId = 1 },
                new SubCategory { Id = 3, Name = "Desktop", Url = "desktop", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Game Computer", Url = "game-pc", CategoryId = 2 },
                new SubCategory { Id = 5, Name = "Computer", Url = "c-game-pc", CategoryId = 3 },
                new SubCategory { Id = 6, Name = "Smarth Phone", Url = "smarthphone", CategoryId = 4},
                new SubCategory { Id = 7, Name = "Phone", Url = "m-phone", CategoryId = 4},
                new SubCategory { Id = 8, Name = "Phone", Url = "p-phone", CategoryId = 5},
                new SubCategory { Id = 9, Name = "Tablet", Url = "tablet", CategoryId = 6},
                new SubCategory { Id = 10, Name = "Full HD", Url = "fullhd", CategoryId = 7},
                new SubCategory { Id = 11, Name = "Projection", Url = "p-projection", CategoryId = 8},
                new SubCategory { Id = 12, Name = "SoundBar", Url = "s-soundbar", CategoryId = 9},
                new SubCategory { Id = 13, Name = "Camera", Url = "c-camera", CategoryId = 10},
                new SubCategory { Id = 14, Name = "Vlog Camera", Url = "vlogcamera", CategoryId = 10},
                new SubCategory { Id = 15, Name = "Camera", Url = "v-camera", CategoryId = 11},
                new SubCategory { Id = 16, Name = "Drone", Url = "drone", CategoryId = 12},
                new SubCategory { Id = 17, Name = "Bluetooth Earphones", Url = "bluetoothearphones", CategoryId = 13},
                new SubCategory { Id = 18, Name = "Microphone", Url = "Microphone", CategoryId = 13},
                new SubCategory { Id = 19, Name = "Case", Url = "case", CategoryId = 14}

                );
        }
    }
}
