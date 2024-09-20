using WeatherAPI.Clients;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.Services;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Configuration;

public static class DependencyConfig
{
    public static void ConfigureDependencies(this IServiceCollection services)
    {
        services.AddHttpClient<IMeteoApiClient, MeteoApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://archive-api.open-meteo.com/");
        });
        services.AddHttpClient<IOpenWeatherApiClient, OpenWeatherApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.openweathermap.org/");
        });
        services.AddHttpClient<IVisualCrossingApiClient, VisualCrossingApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://weather.visualcrossing.com/");
        });
        services.AddHttpClient<ITomorrowApiClient, TomorrowApiClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.tomorrow.io/");
        });
        services.AddScoped<IGeoLocationService, GeoLocationService>();
        services.AddScoped<IWeatherService, WeatherService>();
    }
}