using Ecommerce.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Application.BaseDb;

public class OrderConfig : IEntityTypeConfiguration<CustomerOrder>
{
    public void Configure(EntityTypeBuilder<CustomerOrder> builder)
    {
        builder.Property(o => o.OrderStatus).HasConversion(
            s => s.ToString(),
            x => (OrderStatus)Enum.Parse(typeof(OrderStatus), x));

        builder.OwnsOne(o => o.BillingAddress, onb => { onb.WithOwner(); });
        builder.OwnsOne(o => o.ShippingAddress, onb => { onb.WithOwner(); });

        builder.HasMany(o => o.CustomerOrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderItemConfig : IEntityTypeConfiguration<CustomerOrderItem>
{
    public void Configure(EntityTypeBuilder<CustomerOrderItem> builder)
    {
        builder.OwnsOne(coi => coi.OrderedProduct, onb => { onb.WithOwner(); });
    }
}