using System.Text;
using Ecommerce.Application.DbProvider;
using Ecommerce.Application.Middleware;
using Ecommerce.Auth.Entities;
using Ecommerce.Auth.Services;
using Ecommerce.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;

namespace Ecommerce.Application.Extension;

public static class ForServiceCollection
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        // TODO: Add defined services here
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        services.AddTransient<ApplicationExceptionHandler>();
        services.AddTransient<RouteNotFoundHandler>();

        return services;
    }

    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return configuration.GetValue<string>("DatabaseProvider") switch
        {
            "Postgre" => services.AddDbContext<AppDbContext, PostgreDbContext>(),
            _ => services.AddDbContext<AppDbContext, SqliteDbContext>()
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
            var passwordSettingsConfig = configuration.GetSection("IdentityOptions:Password");

            // Password settings.
            options.Password.RequireDigit = passwordSettingsConfig.GetValue("RequireDigit", options.Password.RequireDigit);
            options.Password.RequireLowercase = passwordSettingsConfig.GetValue("RequireLowercase", options.Password.RequireLowercase);
            options.Password.RequireNonAlphanumeric = passwordSettingsConfig.GetValue("RequireNonAlphanumeric", options.Password.RequireNonAlphanumeric);
            options.Password.RequireUppercase = passwordSettingsConfig.GetValue("RequireUppercase", options.Password.RequireUppercase);
            options.Password.RequiredLength = passwordSettingsConfig.GetValue("RequiredLength", options.Password.RequiredLength);
            options.Password.RequiredUniqueChars = passwordSettingsConfig.GetValue("RequiredUniqueChars", options.Password.RequiredUniqueChars);

            var lockoutSettingsConfig = configuration.GetSection("IdentityOptions:Lockout");

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(lockoutSettingsConfig.GetValue("DefaultLockoutTimeSpanMinutes", options.Lockout.DefaultLockoutTimeSpan.TotalMinutes));
            options.Lockout.MaxFailedAccessAttempts = lockoutSettingsConfig.GetValue("MaxFailedAccessAttempts", options.Lockout.MaxFailedAccessAttempts);
            options.Lockout.AllowedForNewUsers = lockoutSettingsConfig.GetValue("AllowedForNewUsers", options.Lockout.AllowedForNewUsers);

            var userSettingsConfig = configuration.GetSection("IdentityOptions:User");

            // User settings.
            options.User.AllowedUserNameCharacters = userSettingsConfig.GetValue<string>("AllowedUserNameCharacters") ?? options.User.AllowedUserNameCharacters;
            options.User.RequireUniqueEmail = userSettingsConfig.GetValue("RequireUniqueEmail", options.User.RequireUniqueEmail);
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

    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy("DevCor", policy =>
            {
                policy.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
    
    public static IServiceCollection AddRedis(this IServiceCollection services,
                                              IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var connectionString = configuration.GetConnectionString("RedisConn");

            if (string.IsNullOrEmpty(connectionString?.Trim()))
                throw new Exception("Can not find redis connection string");

            var options = ConfigurationOptions.Parse(connectionString);
            return ConnectionMultiplexer.Connect(options);
        });

        return services;
    }
}