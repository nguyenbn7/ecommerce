namespace Ecommerce.Orders.Models;

public class PreviewCustomerOrder
{
    public int OrderId { get; set; }
    public required string BuyerEmail { get; set; }
    public required string OrderStatus { get; set; }
    public required DateTime OrderDate { get; set; }
}