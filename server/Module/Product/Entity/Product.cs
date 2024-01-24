namespace Ecommerce.Module.Product.Entity;

public class Product
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}