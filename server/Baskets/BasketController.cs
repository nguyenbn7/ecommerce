using AutoMapper;
using Ecommerce.Baskets.Entities;
using Ecommerce.Baskets.Models;
using Ecommerce.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Baskets;

public class BasketController(ILogger<BasketController> logger,
                        IBasketRepository basketRepository,
                        IMapper mapper) : APIController(logger)
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasketByIdAsync(string id)
    {
        var basket = await _basketRepository.GetBasketAsync(id);
        return basket ?? new Basket(id);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateBasketAsync(CustomerBasket customerBasket)
    {
        var basket = _mapper.Map<CustomerBasket, Basket>(customerBasket);
        var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketById(string id)
    {
        var deleted = await _basketRepository.DeleteBasketAsync(id);

        if (deleted)
            return Ok();
            
        return NotFound();
    }
}