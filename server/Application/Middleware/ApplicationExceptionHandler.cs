using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Ecommerce.Shared.Models;

namespace Ecommerce.Application.Middleware;

public class ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger,
                        IHostEnvironment environment) : IMiddleware
{
    private readonly ILogger<ApplicationExceptionHandler> _logger = logger;
    private readonly IHostEnvironment _environment = environment;

    public async Task InvokeAsync(HttpContext context,
                                  RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Message: {}", ex.Message);
            _logger.LogError("Details: {}", ex.StackTrace);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,
                                            Exception ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _environment.IsDevelopment()
            ? new ErrorResponse(ex.Message,
                           ex.StackTrace?.ToString() ?? string.Empty)
            : new ErrorResponse(context.Response.StatusCode);

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}