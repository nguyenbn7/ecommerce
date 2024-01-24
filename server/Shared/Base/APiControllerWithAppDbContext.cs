namespace Ecommerce.Shared.Base;

public abstract class APiControllerWithAppDbContext(
    ILogger logger, AppDbContext context) : APIController(logger)
{
    protected readonly AppDbContext context = context;
}