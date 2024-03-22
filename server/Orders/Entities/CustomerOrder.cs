using System.Diagnostics.CodeAnalysis;
using Ecommerce.Auth.Entities;

namespace Ecommerce.Orders.Entities;

public class CustomerOrder
{
    public CustomerOrder()
    {
    }

    [SetsRequiredMembers]
    public CustomerOrder(
        AppUser customer,
        List<CustomerOrderItem> customerOrderItems,
        decimal subTotal,
        OrderAddress shippingAddress,
        OrderAddress billingAddress,
        ShippingMethod shippingMethod)
    {
        SubTotal = subTotal;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        CustomerOrderItems = customerOrderItems;
        ShippingMethod = shippingMethod;
        Customer = customer;
    }

    public int Id { get; set; }
    public decimal SubTotal { get; set; }
    public required OrderAddress ShippingAddress { get; set; }
    public required OrderAddress BillingAddress { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public required AppUser Customer { get; set; }
    public List<CustomerOrderItem> CustomerOrderItems { get; set; } = [];
    public required ShippingMethod ShippingMethod { get; set; }

    public decimal Total
    {
        get => SubTotal + ShippingMethod.Price;
    }
}