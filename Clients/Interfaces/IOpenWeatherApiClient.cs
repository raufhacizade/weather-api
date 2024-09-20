using WeatherAPI.Models;

namespace WeatherAPI.Clients.Interfaces;

public interface IOpenWeatherApiClient
{
    Task<GeoLocationModel?> FetchLatitudeAndLongitudeAsync(string cityName, string countryName);
}