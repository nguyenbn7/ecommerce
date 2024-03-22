namespace Ecommerce.Orders.Entities;

public class OrderAddress
{
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public string? Address2 { get; set; }
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public string? ZipCode { get; set; }
}