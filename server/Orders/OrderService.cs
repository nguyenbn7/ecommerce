using Ecommerce.Auth.Entities;
using Ecommerce.Baskets.Entities;
using Ecommerce.Orders.Entities;
using Ecommerce.Shared.BaseDb;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders;

public class OrderService(AppDbContext dbContext) : IOrderService
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<CustomerOrder?> CreateOrderAsync(
        AppUser customer,
        Basket basket,
        OrderAddress billingAddress,
        OrderAddress shippingAddress,
        ShippingMethod shippingMethod)
    {
        var productIds = basket.Items.Select(bi => bi.Id).ToArray();

        var products = await _dbContext.Products.AsNoTracking().Where(p => productIds.Contains(p.Id)).ToListAsync();

        if (productIds.Length != products.Count)
        {
            var queriedProductIds = products.Select(p => p.Id).ToList();
            var missedProductIds = productIds.Except(queriedProductIds);
            throw new Exception($"Can not find product with ids: [{string.Join(", ", missedProductIds)}]");
        }

        var orderItems = new List<CustomerOrderItem>();
        var subTotal = 0m;

        for (int i = 0; i < products.Count; i++)
        {
            var orderedProduct = new OrderedProduct
            {
                ProductId = products[i].Id,
                ProductName = products[i].Name,
                ProductPictureUrl = products[i].PictureUrl
            };

            var customerOrderItem = new CustomerOrderItem
            {
                OrderedProduct = orderedProduct,
                Price = basket.Items[0].Price,
                Quantity = basket.Items[0].Quantity
            };

            subTotal += customerOrderItem.Price * customerOrderItem.Quantity;

            orderItems.Add(customerOrderItem);
        }

        var order = new CustomerOrder(customer, orderItems, subTotal, shippingAddress, billingAddress, shippingMethod);

        _dbContext.CustomerOrders.Add(order);

        var count = await _dbContext.SaveChangesAsync();

        return count == 0 ? null : order;
    }
}