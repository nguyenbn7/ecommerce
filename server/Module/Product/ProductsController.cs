using Ecommerce.Shared.Base;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Module.Products;

public class ProductsController(ILogger<ProductsController> logger,
    AppDbContext context) : APiControllerWithAppDbContext(logger, context)
{
    [HttpGet]
    public IActionResult GetProductsAsync()
    {
        return Ok("List of products go here");
    }
}