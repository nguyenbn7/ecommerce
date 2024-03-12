using Ecommerce.Baskets.Entities;

namespace Ecommerce.Baskets;

public interface IBasketRepository
{
    Task<Basket?> GetBasketAsync(string basketId);
    Task<Basket?> UpdateBasketAsync(Basket basket);
    Task<bool> DeleteBasketAsync(string basketId);
}