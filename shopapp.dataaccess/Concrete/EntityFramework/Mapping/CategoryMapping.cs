using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping
{
    internal class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id=1,Name="Elektronik",Url="elektronik"},
                new Category { Id=2,Name= "TV", Url="tv"},
                new Category { Id=3,Name= "Bilgisayar", Url="bilgisayar"}
                
                );
        }
    }
}
