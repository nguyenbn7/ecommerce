using System.Text.Json;
using Ecommerce.Baskets.Entities;
using StackExchange.Redis;

namespace Ecommerce.Baskets;

public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
{
    private readonly IDatabase _db = redis.GetDatabase();

    public async Task<bool> DeleteBasketAsync(string basketId)
    {
        return await _db.KeyDeleteAsync(basketId);
    }

    public async Task<Basket?> GetBasketAsync(string basketId)
    {
        var data = await _db.StringGetAsync(basketId);
        if (data.IsNullOrEmpty)
            return null;
        return JsonSerializer.Deserialize<Basket>(data.ToString());
    }

    public async Task<Basket?> UpdateBasketAsync(Basket basket)
    {
        var created = await _db.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

        if (!created) return null;

        return await GetBasketAsync(basket.Id);
    }
}