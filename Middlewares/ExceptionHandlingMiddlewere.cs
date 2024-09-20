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
        _logger.LogError(ex, "An error occurred");

        (int, string) response = ex switch
        {
            ExternalApiRequestException => (StatusCodes.Status503ServiceUnavailable, ex.Message),
            InvalidDataException => (StatusCodes.Status400BadRequest, ex.Message),
            InValidCityOrCountryName => (StatusCodes.Status400BadRequest, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Unexpected error happened")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.Item1;

        var result = JsonSerializer.Serialize(new { error = response.Item2 });
        return context.Response.WriteAsync(result);
    }
}
