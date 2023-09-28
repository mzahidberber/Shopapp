using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class StockMapping : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        //builder.HasOne(x=>x.Product).WithMany(x=>x.Stocks).HasForeignKey(x=>x.ProductId);
        builder.HasData(
            new Stock { Id = 1, Name = "Color", ProductId = 1 },
            new Stock { Id = 2, Name = "Memory", ProductId = 1 }
            );
    }
}
