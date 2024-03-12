using Ecommerce.Shared;
using Ecommerce.Shared.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Products;

public class ProductsController(ILogger<ProductsController> logger,
    AppDbContext context) : APiControllerWithAppDbContext(logger, context)
{
    [HttpGet]
    public IActionResult GetProductsAsync()
    {
        return Ok("List of products go here");
    }
}