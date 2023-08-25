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
                new MainCategory { Id = 1, Name = "Bilgisayar", Url = "bilgisayar" },
                new MainCategory { Id = 2, Name = "Telefon", Url = "telefon" },
                new MainCategory { Id = 3, Name = "TV", Url = "tv" }

                );
        }
    }
}
