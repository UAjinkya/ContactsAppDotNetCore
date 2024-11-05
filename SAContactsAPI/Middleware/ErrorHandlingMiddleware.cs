using System.Net;
using System.Text.Json;

namespace SAContactsAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Process the request
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle exceptions
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // Set default error details
            var errorDetails = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "An unexpected error occurred. Please try again later."
            };

            // Customize response based on exception type if needed
            if (exception is ArgumentException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorDetails = new { StatusCode = response.StatusCode, Message = exception.Message };
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            // Serialize the error response to JSON and return it
            return response.WriteAsync(JsonSerializer.Serialize(errorDetails));
        }
    }
}