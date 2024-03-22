using Ecommerce.Application.Middleware;
using Ecommerce.Application.ServiceCollectionExtension;
using Ecommerce.Application.WebApplicationExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAppSwagger();

builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.UseIdentity(builder.Configuration, builder.Environment);

builder.Services.UseJWTForAuthentication(builder.Configuration);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddRedis(builder.Configuration);

builder.Services.AddAppServices();

builder.Services.ConfigureApiBehaviourOptions();

builder.Services.AddAppCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ApplicationExceptionHandler>();

app.UseMiddleware<RouteNotFoundHandler>();

app.UseStaticFiles();

app.UseCors("DevCor");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.ApplyMigrationsAsync();

await app.CreateSystemAdminAsync();

await app.SeedProductsDataAsync();

await app.SeedDeliveryMethodsAsync();

await app.CreateDemoCustomersAsync();

app.Run();
