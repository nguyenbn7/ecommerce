namespace Ecommerce.Shared.Controllers;

public abstract class APiControllerWithAppDbContext(
    ILogger logger, AppDbContext context) : APIController(logger)
{
    protected readonly AppDbContext context = context;
}