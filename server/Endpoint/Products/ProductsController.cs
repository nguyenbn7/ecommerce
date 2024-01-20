using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Endpoint.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProductsAsync()
    {
        return Ok("List of products go here");
    }
}