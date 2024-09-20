using WeatherAPI.Models;

namespace WeatherAPI.Services.Interfaces;

public interface IWeatherService
{
    Task<WeatherModel?> GetTodayWeatherInfoAsync(string cityName, string countryName);
    Task<WeatherModel?> GetArchivedWeatherInfoByDayAsync(double latitude, double longitude, DateOnly date);
    Task<DetailedWeatherModel?> GetDetailedWeatherInfoByDayAsync(string cityName, string countryName, DateOnly date);
}