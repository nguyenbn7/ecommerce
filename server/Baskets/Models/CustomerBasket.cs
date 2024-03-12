using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Baskets.Models;

public class CustomerBasket
{
    [Required]
    public required string Id { get; set; }

    [Required]
    public required List<CustomerBasketItem> Items { get; set; }
}