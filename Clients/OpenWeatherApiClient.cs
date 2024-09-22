using AutoMapper;
using WeatherAPI.Models;
using WeatherAPI.Clients.Base;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Exceptions;

namespace WeatherAPI.Clients;

public class OpenWeatherApiClient(HttpClient httpClient, IMapper mapper, ILogger<OpenWeatherApiClient> logger)
    : BaseApiClient(httpClient, mapper, logger), IOpenWeatherApiClient
{
    public async Task<GeoLocationModel?> FetchLatitudeAndLongitudeAsync(string cityName, string countryName)
    {
        try
        {
            var query = $"q={cityName},{countryName}&limit=1";
            var baseAddress = _httpClient.BaseAddress;
            var uri = $"{baseAddress?.Scheme}://{baseAddress?.Host}/geo/1.0/direct{baseAddress?.Query}&{query}";

            _logger.LogInformation($"Fetching data from {baseAddress?.Host} with {query} query parameters");
            var locationData = await GetData<List<OpenWeatherGeoLocationDto>?>(uri);
            if (locationData != null && locationData.Count > 0)
            {
                var firstLocation = locationData[0];
                return _mapper.Map<GeoLocationModel>(firstLocation);
            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, $"Failed to retrieve location data from OpenWeatherMap Api,\n{httpEx.Message}");
            throw new ExternalApiRequestException();
            ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching from OpenWeatherMap API");
            throw;
        }

        return null;
    }
}