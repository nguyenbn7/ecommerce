using AutoMapper;
using Ecommerce.Baskets.Entities;
using Ecommerce.Baskets.Models;
using Ecommerce.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Baskets;

public class BasketController : APIController
{
    private readonly IBasketRepository basketRepository;
    private readonly IMapper mapper;

    public BasketController(ILogger<BasketController> logger,
                            IBasketRepository basketRepository,
                            IMapper mapper)
        : base(logger)
    {
        this.basketRepository = basketRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasketByIdAsync(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id);
        return basket ?? new Basket(id);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasketAsync(CustomerBasket customerBasket)
    {
        var basket = mapper.Map<CustomerBasket, Basket>(customerBasket);
        var updatedBasket = await basketRepository.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketById(string id)
    {
        var deleted = await basketRepository.DeleteBasketAsync(id);

        if (deleted)
            return Ok();
            
        return NotFound();
    }
}