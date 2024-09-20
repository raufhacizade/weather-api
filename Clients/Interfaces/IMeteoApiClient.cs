using WeatherAPI.Models;

namespace WeatherAPI.Clients.Interfaces;

public interface IMeteoApiClient
{
    Task<WeatherModel?> FetchArchivedWeatherDataAsync(double latitude, double longitude, DateOnly date);
}