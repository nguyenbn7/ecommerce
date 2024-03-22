using System.Text;
using Ecommerce.Auth.Entities;
using Ecommerce.Shared.BaseDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Application.ServiceCollectionExtension;

public static class AuthenticationConfiguration
{
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