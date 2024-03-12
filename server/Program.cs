using Ecommerce.Application.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddAppSwagger();

builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddAuthentication();

builder.Services.UseIdentity(builder.Configuration, builder.Environment);

builder.Services.UseJWTForAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

await app.ApplyMigration();

await app.CreateSystemRolesAsync();

await app.CreateSystemAdminAsync();

app.Run();
