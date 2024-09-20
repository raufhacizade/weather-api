using WeatherAPI.Models;

namespace WeatherAPI.Services.Interfaces;

public interface IGeoLocationService
{
    Task<GeoLocationModel?> GetLatitudeAndLongitudeAsync(string cityName, string countryName);
}
