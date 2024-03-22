namespace Ecommerce.Shared.Controllers;

public abstract class APiControllerWithAppDbContext(
    ILogger logger, AppDbContext dbContext) : APIController(logger)
{
    protected readonly AppDbContext _dbContext = dbContext;
}