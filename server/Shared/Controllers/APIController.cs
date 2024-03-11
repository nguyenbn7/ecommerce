using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Shared.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class APIController(ILogger logger) : ControllerBase
{
    protected readonly ILogger logger = logger;
}