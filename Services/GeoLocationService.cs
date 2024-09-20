using Microsoft.Extensions.Caching.Memory;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.Models;
using WeatherAPI.Services.Base;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Services;

public class GeoLocationService(
    IOpenWeatherApiClient openWeatherApiClient,
    IMemoryCache cache,
    ILogger<GeoLocationService> logger)
    : BaseCacheableService(cache, logger), IGeoLocationService
{
    private readonly IMemoryCache _cache = cache;
    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        Priority = CacheItemPriority.NeverRemove
    };

    public async Task<GeoLocationModel?> GetLatitudeAndLongitudeAsync(string cityName, string countryName)
    {
        if (_cache.TryGetValue((cityName, countryName), out (double latitude, double longitude) cachedLocation))
        {
            return new(cachedLocation);
        }

        var location = await openWeatherApiClient.FetchLatitudeAndLongitudeAsync(cityName, countryName);
        if (location != null)
        {
            _cache.Set((cityName, countryName), location, _cacheEntryOptions);
        }

        return location;
    }
}