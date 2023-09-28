using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class StockValueMapping : IEntityTypeConfiguration<StockValue>
{
    public void Configure(EntityTypeBuilder<StockValue> builder)
    {
        builder.HasOne(x => x.Stock).WithMany(x => x.StockValues).HasForeignKey(x => x.StockId);
        builder.HasData(
            new StockValue { Id = 1, StockId = 1, Name = "Blue", Value = 10 },
            new StockValue { Id = 2, StockId = 1, Name = "Red", Value = 20 },
            new StockValue { Id = 3, StockId = 2, Name = "500GB", Value = 30 },
            new StockValue { Id = 4, StockId = 2, Name = "1TB", Value = 15 }
            );
    }
}
