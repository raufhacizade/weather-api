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
    private readonly string _apiKey = "a0a675610cf629edd2eead4e62063da4";

    public async Task<GeoLocationModel?> FetchLatitudeAndLongitudeAsync(string cityName, string countryName)
    {
        try
        {
            var url = _httpClient.BaseAddress + $"geo/1.0/direct?q={cityName},{countryName}&limit=1";

            _logger.LogInformation($"Fetching data from {url}");
            var locationData = await GetData<List<OpenWeatherGeoLocationDto>?>($"{url}&appid={_apiKey}");
            if (locationData != null && locationData.Count > 0)
            {
                var firstLocation = locationData[0];
                return _mapper.Map<GeoLocationModel>(firstLocation);
            }
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, $"Failed to retrieve location data from OpenWeatherMap Api,\n{httpEx.Message}");
            throw new ExternalApiRequestException();;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching from OpenWeatherMap API");
            throw;
        }

        return null;
    }
}