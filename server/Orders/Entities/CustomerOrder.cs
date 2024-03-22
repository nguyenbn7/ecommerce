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
        string buyerEmail,
        decimal subTotal,
        ShippingAddress shippingAddress,
        BillingAddress billingAddress,
        List<CustomerOrderItem> customerOrderItems,
        ShippingMethod shippingMethod,
        AppUser customer)
    {
        BuyerEmail = buyerEmail;
        SubTotal = subTotal;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        CustomerOrderItems = customerOrderItems;
        ShippingMethod = shippingMethod;
        Customer = customer;
    }

    public int Id { get; set; }
    public required string BuyerEmail { get; set; }
    public decimal SubTotal { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public required ShippingAddress ShippingAddress { get; set; }
    public required BillingAddress BillingAddress { get; set; }
    public List<CustomerOrderItem> CustomerOrderItems { get; set; } = [];
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public string? PaymentIntentId { get; set; }
    public required ShippingMethod ShippingMethod { get; set; }
    public required AppUser Customer { get; set; }

    public decimal Total
    {
        get => SubTotal + ShippingMethod.Price;
    }
}