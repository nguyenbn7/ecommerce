using System.Text;
using Ecommerce.Application.DbProvider;
using Ecommerce.Authorization.Entities;
using Ecommerce.Authorization.Services;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Ecommerce.Application.Extension;

public static class ForServiceCollection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // TODO: Add defined services here
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        
        return services;
    }

    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return configuration.GetValue<string>("DatabaseProvider") switch
        {
            "Sqlite" => services.AddDbContext<AppDbContext, SqliteDbContext>(),
            _ => services.AddDbContext<AppDbContext, PostgreDbContext>()
        };
    }

    public static IServiceCollection UseIdentity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
    {
        services.AddIdentityCore<AppUser>(options =>
        {
            // Add options here
        })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            // Configure more if needed
            var passwordSettingsConfig = configuration.GetSection("Identity:Options:Password");

            // Password settings.
            options.Password.RequireDigit = !env.IsDevelopment();
            options.Password.RequireLowercase = !env.IsDevelopment();
            options.Password.RequireNonAlphanumeric = !env.IsDevelopment();
            options.Password.RequireUppercase = !env.IsDevelopment();
            options.Password.RequiredLength = passwordSettingsConfig.GetValue<int>("RequiredLength");
            options.Password.RequiredUniqueChars = passwordSettingsConfig.GetValue<int>("RequiredUniqueChars");

            var lockoutSettingsConfig = configuration.GetSection("Identity:Options:Lockout");

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettingsConfig.GetValue<double>("DefaultLockoutTimeSpanMinutes"));
            options.Lockout.MaxFailedAccessAttempts = lockoutSettingsConfig.GetValue<int>("MaxFailedAccessAttempts");
            options.Lockout.AllowedForNewUsers = lockoutSettingsConfig.GetValue<bool>("AllowedForNewUsers");

            var userSettingsConfig = configuration.GetSection("Identity:Options:User");

            // User settings.
            options.User.AllowedUserNameCharacters = userSettingsConfig.GetValue<string>("AllowedUserNameCharacters") ?? options.User.AllowedUserNameCharacters;
            options.User.RequireUniqueEmail = userSettingsConfig.GetValue<bool>("RequireUniqueEmail");
        });

        return services;
    }

    public static IServiceCollection AddAppSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authentication Bearer Scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    securitySchema, ["Bearer"]
                }
            };

            c.AddSecurityRequirement(securityRequirement);
        });

        return services;
    }

    public static IServiceCollection UseJWTForAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettingsConfig = configuration.GetSection("JWT");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var appKey = tokenSettingsConfig["Key"];
                var appIssuer = tokenSettingsConfig["Issuer"];
                if (appKey == null)
                {
                    throw new Exception("JWT Key not found");
                }

                if (appIssuer == null)
                {
                    throw new Exception("JWT Issuer not found");
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appKey)),
                    ValidIssuer = appIssuer,
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });

        return services;
    }
}