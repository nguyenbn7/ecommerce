using Ecommerce.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Module.Products;

public class ProductsController(AppDbContext context) : APiControllerWithAppDbContext(context)
{
    [HttpGet]
    public IActionResult GetProductsAsync()
    {
        return Ok("List of products go here");
    }
}