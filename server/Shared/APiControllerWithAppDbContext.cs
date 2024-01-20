namespace Ecommerce.Shared;

public abstract class APiControllerWithAppDbContext(AppDbContext context) : APIController
{
    protected readonly AppDbContext _context = context;
}