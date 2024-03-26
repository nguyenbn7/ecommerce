using Ecommerce.Orders.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Application.DbPostgre;

public class CustomerOrderItemConfiguration : IEntityTypeConfiguration<CustomerOrderItem>
{
    public void Configure(EntityTypeBuilder<CustomerOrderItem> builder)
    {
        builder.Property(coi => coi.Price).HasColumnType("decimal(18,2)");
    }
}