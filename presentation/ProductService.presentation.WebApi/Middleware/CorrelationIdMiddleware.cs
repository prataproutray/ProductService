namespace ProductService.presentation.WebApi.Middleware
{
    public class CorrelationIdMiddleware
    {
        RequestDelegate _next;
        ILogger<CorrelationIdMiddleware> _logger;


        public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Correlation-ID"))
            {
                context.Request.Headers["X-Correlation-ID"] = Guid.NewGuid().ToString();
            }
            _logger.LogInformation("Correlation ID: {CorrelationId}", context.Request.Headers["X-Correlation-ID"].ToString());
            context.Response.OnStarting(() =>
            {
                context.Response.Headers["X-Correlation-ID"] = context.Request.Headers["X-Correlation-ID"];
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}