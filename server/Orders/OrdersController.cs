using System.Security.Claims;
using AutoMapper;
using Ecommerce.Auth.Entities;
using Ecommerce.Baskets;
using Ecommerce.Orders.Entities;
using Ecommerce.Orders.Models;
using Ecommerce.Shared.BaseDb;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Orders;

public class OrdersController(
    ILogger<OrdersController> logger,
    AppDbContext context,
    IBasketRepository basketRepository,
    IOrderService orderService,
    IMapper mapper,
    UserManager<AppUser> userManager) : APiControllerWithAppDbContext(logger, context)
{
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IOrderService _orderService = orderService;
    private readonly IMapper _autoMapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;

    [Authorize]
    [HttpGet("Preview")]
    public async Task<ActionResult<PreviewCustomerOrder>> PreviewOrderHistoriesAsync()
    {
        var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (email == null)
            return Unauthorized();

        var customer = await _userManager.FindByEmailAsync(email);

        if (customer == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(customer);
        if (!roles.Contains("Customer"))
            return Forbid();

        var orders = await _dbContext.CustomerOrders.AsNoTracking()
            .Where(o => o.Customer.Id == customer.Id)
            .Select(o => new PreviewCustomerOrder
            {
                OrderId = o.Id,
                OrderStatus = o.OrderStatus.ToString(),
                OrderDate = o.OrderDate
            }).ToListAsync();

        return Ok(orders);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrderAsync(CreateOrderRequest orderRequest)
    {
        var email = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (email == null)
            return Unauthorized();

        var customer = await _userManager.FindByEmailAsync(email);

        if (customer == null)
            return Unauthorized();

        var roles = await _userManager.GetRolesAsync(customer);
        if (!roles.Contains("Customer"))
            return Forbid();

        var basket = await _basketRepository.GetBasketAsync(orderRequest.BasketId);

        if (basket == null)
            return BadRequest(new ErrorResponse(400, $"Basket with id '{orderRequest.BasketId}' not found"));

        var shippingMethod = await _dbContext.ShippingMethods.FirstOrDefaultAsync(sm => sm.Id == orderRequest.DeliveryMethodId);

        if (shippingMethod == null)
            return BadRequest(new ErrorResponse(400, $"Shipping Method Id '{orderRequest.DeliveryMethodId}' not found"));

        var billingAddress = _autoMapper.Map<AddressDTO, OrderAddress>(orderRequest.BillingAddress);

        OrderAddress shippingAddress;
        if (orderRequest.ShippingAdress != null)
        {
            shippingAddress = _autoMapper.Map<AddressDTO, OrderAddress>(orderRequest.ShippingAdress);
        }
        else
        {
            shippingAddress = _autoMapper.Map<AddressDTO, OrderAddress>(orderRequest.BillingAddress);
        }

        var order = await _orderService.CreateOrderAsync(
            customer,
            basket,
            billingAddress,
            shippingAddress,
            shippingMethod);

        if (order == null)
            return BadRequest(new ErrorResponse(400, "Can not create an order. Please contact to our support for more details"));

        await _basketRepository.DeleteBasketAsync(orderRequest.BasketId);

        return Ok(new
        {
            OrderId = order.Id
        });
    }

    [HttpGet("Shipping/Methods")]
    public async Task<IActionResult> GetShippingMethodsAsync()
    {
        return Ok(await _dbContext.ShippingMethods.ToListAsync());
    }

    [HttpGet("Payment/Types")]
    public ActionResult<List<string>> GetPaymentTypes()
    {
        var payments = new List<string>
        {
            "Cash",
            "Credit",
            "Debit"
        };
        return payments;
    }
}