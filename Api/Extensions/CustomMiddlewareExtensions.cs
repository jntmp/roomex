namespace Api.Extensions;

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HttpLoggingMiddleware>();
    }
}
