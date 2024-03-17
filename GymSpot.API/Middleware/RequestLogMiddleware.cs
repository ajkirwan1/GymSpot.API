namespace GymSpot.API.Middleware
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<RequestLogMiddleware> logger)
        {
            logger.LogInformation("Request received: {Method} {Path}", context.Request.Method, context.Request.Path);
            await _next(context);
            logger.LogInformation("Response sent: {StatusCode}", context.Response.StatusCode);
        }

    }
}
