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
                new Category { Id = 2, Name = "Masaüstü Bilgisayar", Url = "masaustupc",MainCategoryId=1 },
                new Category { Id = 3, Name = "Cep Telefonu", Url = "ceptelefonu",MainCategoryId=2 },
                new Category { Id = 4, Name = "Tablet", Url = "tablet",MainCategoryId=2 },
                new Category { Id = 5, Name = "Televizyon", Url = "televizyon",MainCategoryId=3}

                );
        }
    }
}
