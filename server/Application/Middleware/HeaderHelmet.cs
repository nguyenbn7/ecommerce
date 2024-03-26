using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Ecommerce.Application.Middleware;

public class HeaderHelmet : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers[HeaderNames.ContentSecurityPolicy] = "default-src 'self';base-uri 'self';font-src 'self' https: data:;form-action 'self';frame-ancestors 'self';img-src 'self' data:;object-src 'none';script-src 'self';script-src-attr 'none';style-src 'self' https: 'unsafe-inline';upgrade-insecure-requests";
        context.Response.Headers["Cross-Origin-Opener-Policy"] = "cross-origin";
        context.Response.Headers["Cross-Origin-Resource-Policy"] = "cross-origin";
        context.Response.Headers["Cross-Origin-Resource-Policy"] = "cross-origin";
        context.Response.Headers["Origin-Agent-Cluster"] = "?1";
        context.Response.Headers["Referrer-Policy"] = "no-referrer";
        context.Response.Headers[HeaderNames.StrictTransportSecurity] = "max-age=15552000; includeSubDomains";
        context.Response.Headers[HeaderNames.XContentTypeOptions] = "nosniff";
        context.Response.Headers["X-DNS-Prefetch-Control"] = "off";
        context.Response.Headers["X-Download-Options"] = "noopen";
        context.Response.Headers[HeaderNames.XFrameOptions] = "SAMEORIGIN";
        context.Response.Headers["X-Permitted-Cross-Domain-Policies"] = "none";
        context.Response.Headers[HeaderNames.XXSSProtection] = "0";

        await next(context);
    }
}