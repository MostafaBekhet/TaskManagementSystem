namespace TaskManagementSystem.API.Middlewares
{
    public class RequestTimeLoggingMiddleWare
    {
        private readonly RequestDelegate _next;

        public RequestTimeLoggingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            await _next(context);

            var processingTime = DateTime.UtcNow - startTime;
            Console.WriteLine($"Request processing time: {processingTime.TotalMilliseconds} ms");
        }
    }
}
