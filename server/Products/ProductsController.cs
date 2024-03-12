using AutoMapper;
using Ecommerce.Products.Entities;
using Ecommerce.Products.Models;
using Ecommerce.Shared;
using Ecommerce.Shared.Controllers;
using Ecommerce.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Products;

public class ProductsController(ILogger<ProductsController> logger,
    AppDbContext context, IMapper mapper) : APiControllerWithAppDbContext(logger, context)
{
    private readonly IMapper mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<Page<ProductDto>>> GetProductsAsync([FromQuery] ProductsQuery query)
    {
        var queryable = context.Products.AsNoTracking().AsQueryable();

        if (query.BrandId.HasValue)
            queryable = queryable.Where(p => p.ProductBrandId == query.BrandId);

        if (query.TypeId.HasValue)
            queryable = queryable.Where(p => p.ProductTypeId == query.TypeId);

        if (query.Search != null)
            queryable = queryable.Where(p => p.Name.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(query.Sort))
            queryable = query.Sort.ToLower() switch
            {
                "price" => queryable.OrderBy(p => p.Price),
                "-price" => queryable.OrderByDescending(p => p.Price),
                _ => queryable.OrderBy(p => p.Name),
            };
        else
            queryable = queryable.OrderBy(p => p.Name);

        var totalItems = await queryable.CountAsync();

        queryable = queryable.Include(p => p.ProductBrand).Include(p => p.ProductType);
        queryable = queryable.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize);

        var data = await queryable.ToListAsync();

        return new Page<ProductDto>
        {
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalItems = totalItems,
            Data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(data)
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var queryable = context.Products.AsNoTracking().AsQueryable();

        queryable = queryable.Include(p => p.ProductBrand).Include(p => p.ProductType);

        var product = await queryable.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound(new ErrorResponse("Product does not exist"));

        return mapper.Map<Product, ProductDto>(product);
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return await context.ProductTypes.AsNoTracking().ToListAsync();
    }

    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return await context.ProductBrands.AsNoTracking().ToListAsync();
    }
}