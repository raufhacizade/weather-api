using Microsoft.Extensions.Caching.Memory;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.Models;
using WeatherAPI.Services.Base;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services;

public class WeatherService(
    IVisualCrossingApiClient visualCrossingApiClient,
    IMeteoApiClient meteoApiClient,
    ITomorrowApiClient tomorrowApiClient,
    IMemoryCache cache,
    ILogger<WeatherService> logger)
    : BaseCacheableService(cache, logger), IWeatherService
{
    private readonly IMemoryCache _cache = cache;

    public async Task<WeatherModel?> GetTodayWeatherInfoAsync(string cityName, string countryName)
    {
        DateTime now = DateTime.Now;
        var date = DateOnly.FromDateTime(now);
        if (_cache.TryGetValue((cityName, countryName, date), out WeatherModel? cachedWeatherData))
        {
            return cachedWeatherData;
        }
        
        var todayWeatherData = await tomorrowApiClient.FetchTodayWeatherByCity(cityName, countryName);
        if (todayWeatherData != null)
        {
            TimeSpan remainingTimeOfTheDay = now.Date.AddDays(1) - now;
            _cache.Set((cityName, countryName, date), todayWeatherData, remainingTimeOfTheDay);
        }

        return todayWeatherData;
    }

    public async  Task<WeatherModel?> GetArchivedWeatherInfoByDayAsync(double latitude, double longitude, DateOnly date)
    {
        return await meteoApiClient.FetchArchivedWeatherDataAsync(latitude, longitude, date);
    }

    public async Task<DetailedWeatherModel?> GetDetailedWeatherInfoByDayAsync(string cityName, string countryName, DateOnly date)
    {
        return await visualCrossingApiClient.FetchWeatherData(cityName, countryName, date);
    }
}