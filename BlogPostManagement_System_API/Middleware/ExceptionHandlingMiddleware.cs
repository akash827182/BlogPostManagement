using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;


namespace BlogPostManagement.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env; 

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env; 
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, message) = GetResponseDetails(exception);

            context.Response.StatusCode = statusCode;

            var response = new
            {
                error = message,
                details = _env.IsDevelopment() ? exception.StackTrace : null 
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }

        private (int statusCode, string message) GetResponseDetails(Exception exception)
        {
            return exception switch
            {
                KeyNotFoundException _ => ((int)HttpStatusCode.NotFound, "Resource not found."),
                ArgumentException _ or ValidationException _ => ((int)HttpStatusCode.BadRequest, exception.Message), 
                UnauthorizedAccessException _ => ((int)HttpStatusCode.Forbidden, "Access denied."),
                _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };
        }
    }
}
