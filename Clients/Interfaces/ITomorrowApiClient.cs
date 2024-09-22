using WeatherAPI.Models;

namespace WeatherAPI.Clients.Interfaces;

public interface ITomorrowApiClient
{
    Task<WeatherModel?> FetchTodayWeatherByCity(double latitude, double longitude);
}