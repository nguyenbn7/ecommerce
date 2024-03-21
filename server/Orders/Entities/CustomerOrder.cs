namespace Ecommerce.Orders.Entities;

public class CustomerOrder
{
    public CustomerOrder()
    {
    }

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
    public string BuyerEmail { get; set; }
    public decimal SubTotal { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public ShippingAddress ShippingAddress { get; set; }
    public BillingAddress BillingAddress { get; set; }
    public List<CustomerOrderItem> CustomerOrderItems { get; set; } = [];
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public string? PaymentIntentId { get; set; }
    public ShippingMethod ShippingMethod { get; set; }

    public decimal Total
    {
        get => SubTotal + ShippingMethod.Price;
    }
}