using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Financ.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled Exception Occurred");

            context.Response.ContentType = "application/json";

            var statusCode = ex switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            object responseBody;

            if (ex is ValidationException validationEx)
            {
                responseBody = new
                {
                    StatusCode = statusCode,
                    Message = "Validation failed",
                    Errors = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                };
            }
            else
            {
                responseBody = new
                {
                    StatusCode = statusCode,
                    Message = ex.Message
                };
            }

            var json = JsonSerializer.Serialize(responseBody, options);
            await context.Response.WriteAsync(json);
        }
    }
}
