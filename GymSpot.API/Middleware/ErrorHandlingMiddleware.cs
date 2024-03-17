namespace GymSpot.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch (Exception ex)
            {
                // Log error
                if (httpContext.Response.StatusCode == 404)
                {
                    // Rediirect to 404 page
                    httpContext.Response.Redirect("/error/404");
                    _logger.LogWarning("An exception occured: {ex}", ex);
                }
                else
                {
                    // Handle other errors
                    _logger.LogWarning("An exception occured: {ex}", ex);
                    httpContext.Response.StatusCode = 500;
                    httpContext.Response.ContentType = "text/plain";
                    await httpContext.Response.WriteAsync("An unexpected error occurred!");
                }
            }
        }

    }
}
