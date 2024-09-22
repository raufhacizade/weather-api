using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using WeatherAPI.Clients;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.Services;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Configuration;

public static class DependencyConfig
{
    public static void ConfigureDependencies(this IServiceCollection services, ConfigurationManager configuration, SecretClient secretClient)
    {
        services.AddHttpClient<IMeteoApiClient, MeteoApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("Clients:Meteo:BaseAddress").Value!);
        });
        services.AddHttpClient<IOpenWeatherApiClient, OpenWeatherApiClient>(client =>
        {
            client.BaseAddress = new Uri($"{configuration.GetSection("Clients:OpenWeather:BaseAddress").Value!}/?appid={secretClient.GetSecret("OpenWeatheApiKey").Value.Value}");
        });
        services.AddHttpClient<IVisualCrossingApiClient, VisualCrossingApiClient>(client =>
        {
            client.BaseAddress = new Uri($"{configuration.GetSection("Clients:VisualCrossing:BaseAddress").Value!}?key={secretClient.GetSecret("VisualCrossingApiKey").Value.Value}");
        });
        services.AddHttpClient<ITomorrowApiClient, TomorrowApiClient>(client =>
        {
            client.BaseAddress = new Uri($"{configuration.GetSection("Clients:Tomorrow:BaseAddress").Value!}?apikey={secretClient.GetSecret("TomorrowApiKey").Value.Value}");
        });
        services.AddScoped<IGeoLocationService, GeoLocationService>();
        services.AddScoped<IWeatherService, WeatherService>();
    }
}