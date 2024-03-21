namespace Ecommerce.Orders.Models;

public class CreateOrderRequest
{
    public required string BuyerEmail { get; set; }
    public required string BasketId { get; set; }
    public int DeliveryMethodId { get; set; }
    public required AddressDTO BillingAddress { get; set; }
    public AddressDTO? ShippingAdress { get; set; }
}