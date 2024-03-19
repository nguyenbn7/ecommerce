using Ecommerce.Shared;
using Ecommerce.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Orders;

public class OrdersController : APiControllerWithAppDbContext
{
    public OrdersController(ILogger logger, AppDbContext context) : base(logger, context)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync()
    {
        return Ok();
    }
}