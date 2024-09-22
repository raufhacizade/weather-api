using AutoMapper;
using WeatherAPI.Clients.Base;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Exceptions;
using WeatherAPI.Models;

namespace WeatherAPI.Clients;

public class TomorrowApiClient(HttpClient httpClient, IMapper mapper, ILogger<TomorrowApiClient> logger)
    : BaseApiClient(httpClient, mapper, logger), ITomorrowApiClient
{
    private readonly string _apiKey = "eAVOps7mSnmFjloqbk8YklN2bMiG5qy2";

    public async Task<WeatherModel?> FetchTodayWeatherByCity(double latitude, double longitude)
    {
        try
        {
            var query = $"location={latitude},{longitude}";
            var baseAddress = _httpClient.BaseAddress;
            var uri = $"{baseAddress?.Scheme}://{baseAddress?.Host}/v4/weather/forecast{baseAddress?.Query}&{query}";

            _logger.LogInformation($"Fetching data from {baseAddress?.Host} with {query} query parameters");
            var responseDto = await GetData<TomorrowResponseDto>(uri);
            return _mapper.Map<WeatherModel>(responseDto);
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, $"Failed to retrieve current weather data from Tomorrow Api,\n{httpEx.Message}");
            throw new ExternalApiRequestException();
            ;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching from Tomorrow API");
            throw;
        }
    }
}