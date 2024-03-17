using GymSpot.API.Middleware;

namespace GymSpot.API.ExtensionMethods
{
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
