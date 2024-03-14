using Ecommerce.Application.Extension;
using Ecommerce.Application.Middleware;

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

builder.Services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ApplicationExceptionHandler>();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<RouteNotFoundHandler>();

app.MapControllers();

await app.ApplyMigration();

await app.CreateSystemAdminAsync();

await app.SeedFakeDataAsync();

app.Run();
