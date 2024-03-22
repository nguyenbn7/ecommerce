using Ecommerce.Auth.Entities;
using Ecommerce.Baskets.Entities;
using Ecommerce.Orders.Entities;

namespace Ecommerce.Orders;

public interface IOrderService
{
    Task<CustomerOrder?> CreateOrderAsync(
        AppUser customer,
        Basket basket,
        OrderAddress billingAddress,
        OrderAddress shippingAddress,
        ShippingMethod shippingMethod);
    // Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    // Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
}