using AutoMapper;
using WeatherAPI.Clients.Base;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Exceptions;
using WeatherAPI.Models;

namespace WeatherAPI.Clients;

public class VisualCrossingApiClient(HttpClient httpClient, IMapper mapper, ILogger<VisualCrossingApiClient> logger)
    : BaseApiClient(httpClient, mapper,
        logger), IVisualCrossingApiClient
{
    private readonly string _apiKey = "2B8W8SHTXX4FUJRVKAEM5GVVJ";

    public async Task<DetailedWeatherModel?> FetchWeatherData(string cityName, string countryName, DateOnly date)
    {
        try
        {
            var path =
                $"VisualCrossingWebServices/rest/services/timeline/{cityName},{countryName}/{date.ToString("yyyy-MM-dd")}";
            var baseAddress = _httpClient.BaseAddress;
            var uri = $"{baseAddress?.Scheme}://{baseAddress?.Host}/{path}/{baseAddress?.Query}";

            _logger.LogInformation($"Fetching data from {baseAddress?.Host}");
            var responseDto = await GetData<VisualCrossingResponseDto>(uri);
            return _mapper.Map<DetailedWeatherModel>(responseDto);
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, $"Failed to retrieve weather data from VisualCrossing Api,\n{httpEx.Message}");
            throw new ExternalApiRequestException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching from VisualCrossing API");
            throw;
        }
    }
}