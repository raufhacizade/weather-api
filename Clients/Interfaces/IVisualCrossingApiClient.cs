using WeatherAPI.Models;

namespace WeatherAPI.Clients.Interfaces;

public interface IVisualCrossingApiClient
{
    Task<DetailedWeatherModel?> FetchWeatherData(string cityName, string countryName, DateOnly date);
}