using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Orders.Entities;

public class CustomerOrder
{
    public CustomerOrder()
    {
    }

    [SetsRequiredMembers]
    public CustomerOrder(string buyerEmail, decimal subTotal, ShippingAddress shippingAddress, BillingAddress billingAddress, List<CustomerOrderItem> customerOrderItems, ShippingMethod shippingMethod)
    {
        BuyerEmail = buyerEmail;
        SubTotal = subTotal;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        CustomerOrderItems = customerOrderItems;
        ShippingMethod = shippingMethod;
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

    public decimal Total
    {
        get => SubTotal + ShippingMethod.Price;
    }
}