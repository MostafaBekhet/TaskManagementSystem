namespace TaskManagementSystem.API.Middlewares
{
    public class ErrorHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Error occurred: " + ex.Message);
            }
        }
    }
}
