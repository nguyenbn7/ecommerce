namespace Ecommerce.Orders.Entities;

public class OrderedProduct
{
    public OrderedProduct()
    {
    }

    public OrderedProduct(int productId, string productName, string productPictureUrl)
    {
        ProductId = productId;
        ProductName = productName;
        ProductPictureUrl = productPictureUrl;
    }

    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductPictureUrl { get; set; }
}