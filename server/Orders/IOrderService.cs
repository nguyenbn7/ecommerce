using Ecommerce.Baskets.Entities;
using Ecommerce.Orders.Entities;

namespace Ecommerce.Orders;

public interface IOrderService
{
    Task<CustomerOrder?> CreateOrderAsync(string buyerEmail, Basket basket, BillingAddress billingAddress, ShippingAddress shippingAddress, ShippingMethod shippingMethod);
    // Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
    // Task<Order> GetOrderByIdAsync(int id, string buyerEmail);
}