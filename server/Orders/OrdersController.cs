using AutoMapper;
using Ecommerce.Baskets;
using Ecommerce.Orders.Entities;
using Ecommerce.Orders.Models;
using Ecommerce.Shared;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders;

public class OrdersController(ILogger logger, AppDbContext context, IBasketRepository basketRepository, IOrderService orderService, IMapper mapper) : APiControllerWithAppDbContext(logger, context)
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IOrderService _orderService = orderService;
    private readonly IMapper _autoMapper = mapper;

    [HttpPost]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest orderRequest)
    {
        var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId);

        if (basket == null)
            return BadRequest(new ErrorResponse(400, $"Basket with id '{orderRequest.BasketId}' not found"));

        var shippingMethod = await _context.ShippingMethods.FirstOrDefaultAsync(sm => sm.Id == orderRequest.DeliveryMethodId);

        if (shippingMethod == null)
            return BadRequest(new ErrorResponse(400, $"Shipping Method Id '{orderRequest.DeliveryMethodId}' not found"));

        var billingAddress = _autoMapper.Map<AddressDTO, BillingAddress>(orderRequest.BillingAddress);

        ShippingAddress shippingAddress;
        if (orderRequest.ShippingAdress != null)
        {
            shippingAddress = _autoMapper.Map<AddressDTO, ShippingAddress>(orderRequest.ShippingAdress);
        }
        else
        {
            shippingAddress = _autoMapper.Map<AddressDTO, ShippingAddress>(orderRequest.BillingAddress);
        }

        var order = await _orderService.CreateOrderAsync(orderRequest.BuyerEmail, basket, billingAddress, shippingAddress, shippingMethod);

        if (order == null)
            return BadRequest(new ErrorResponse(400, "Can not create an order. Please contact to our support for more details"));

        return Ok(new
        {
            OrderId = order.Id
        });
    }

    [HttpGet("Shipping/Method")]
    public async Task<IActionResult> GetShippingMethods()
    {
        return Ok(await _context.ShippingAddresses.ToListAsync());
    }
}