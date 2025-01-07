using System.Net;
using System.Text.Json;

namespace TagerProject.Middleware
{
    public class UnauthorizedResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public UnauthorizedResponseMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized) 
            {
                context.Response.ContentType = "application/json";

                var errorMessage = new
                {
                    satusCode = 401,
                    Message = "Access denied. You are not authorized to perform this action."
                };
                var errorJson = JsonSerializer.Serialize(errorMessage);
                
                // Write the sustome error response
                await context.Response.WriteAsync(errorJson);
            }

        }
    }
}
