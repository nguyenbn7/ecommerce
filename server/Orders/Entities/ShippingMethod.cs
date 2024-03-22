namespace Ecommerce.Orders.Entities;

public class ShippingMethod
{
    public int Id { get; set; }
    public required string ShortName { get; set; }
    public required string DeliveryTime { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
}