using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shopapp.core.Entity.Concrete;

namespace shopapp.dataaccess.Concrete.EntityFramework.Mapping;

internal class OrderProductMapping : IEntityTypeConfiguration<OrderProduct>
{

    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.HasKey(sc => new { sc.OrderId, sc.ProductId });
    }
}
