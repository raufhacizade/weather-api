using System.Text.Json;
using WeatherAPI.Exceptions;

namespace WeatherAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly ILogger _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        // Log the exception (optional)
        _logger.LogError(ex, "An error occurred");

        // Determine the status code and response based on exception type
        (int, string) response = ex switch
        {
            ArgumentException => (StatusCodes.Status400BadRequest, ex.Message),
            ExternalApiRequestException => (StatusCodes.Status503ServiceUnavailable, ex.Message),
            InvalidDataException => (StatusCodes.Status400BadRequest, ex.Message),
            InValidCityOrCountryName => (StatusCodes.Status400BadRequest, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, ex.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.Item1;

        // You can customize the response message as needed
        var result = JsonSerializer.Serialize(new { error = response.Item2 });
        return context.Response.WriteAsync(result);
    }
}
