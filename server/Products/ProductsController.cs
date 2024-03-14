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
    AppDbContext context, IMapper mapper, IConfiguration configuration) : APiControllerWithAppDbContext(logger, context)
{
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _configuration = configuration;

    [HttpGet]
    public async Task<ActionResult<Page<ProductDto>>> GetProductsAsync([FromQuery] ProductsQuery query)
    {
        var queryable = _context.Products.AsNoTracking().AsQueryable();

        if (query.BrandId.HasValue)
            queryable = queryable.Where(p => p.ProductBrandId == query.BrandId);

        if (query.TypeId.HasValue)
            queryable = queryable.Where(p => p.ProductTypeId == query.TypeId);

        if (query.Search != null)
        {
            var dbProvider = _configuration.GetValue<string>("DatabaseProvider");

            if (dbProvider == "Postgre")
                queryable = queryable.Where(p => EF.Functions.ILike(p.Name, $"%{query.Search}%"));
            else
                queryable = queryable.Where(p => EF.Functions.Like(p.Name, $"%{query.Search}%"));
        }

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
            PageSize = query.PageSize > totalItems ? totalItems : query.PageSize,
            TotalItems = totalItems,
            Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(data)
        };
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var queryable = _context.Products.AsNoTracking().AsQueryable();

        queryable = queryable.Include(p => p.ProductBrand).Include(p => p.ProductType);

        var product = await queryable.FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound(new ErrorResponse("Product does not exist"));

        return _mapper.Map<Product, ProductDto>(product);
    }

    [HttpGet("Types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return await _context.ProductTypes.AsNoTracking().ToListAsync();
    }

    [HttpGet("Brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return await _context.ProductBrands.AsNoTracking().ToListAsync();
    }
}