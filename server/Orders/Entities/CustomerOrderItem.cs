namespace Ecommerce.Orders.Entities;

public class CustomerOrderItem
{
    public CustomerOrderItem()
    {
    }

    public CustomerOrderItem(decimal price, int quantity, OrderedProduct orderedProduct)
    {
        Price = price;
        Quantity = quantity;
        OrderedProduct = orderedProduct;
    }

    public int Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    public required OrderedProduct OrderedProduct { get; set; }
}