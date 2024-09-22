using AutoMapper;
using WeatherAPI.Clients.Base;
using WeatherAPI.Clients.Interfaces;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Exceptions;
using WeatherAPI.Models;

namespace WeatherAPI.Clients;

public class MeteoApiClient(HttpClient httpClient, IMapper mapper, ILogger<MeteoApiClient> logger)
    : BaseApiClient(httpClient, mapper, logger), IMeteoApiClient
{
    public async Task<WeatherModel?> FetchArchivedWeatherDataAsync(double latitude, double longitude, DateOnly date)
    {
        var dataStr = date.ToString("yyyy-MM-dd");

        // add latitude, longitude and the date
        string query =
            $"latitude={Math.Round(latitude, 2)}&longitude={Math.Round(longitude, 2)}&start_date={dataStr}&end_date={dataStr}";

        // add other query params
        query +=
            "&daily=temperature_2m_mean,apparent_temperature_mean,wind_speed_10m_max,rain_sum,wind_speed_10m_max,wind_direction_10m_dominant";

        try
        {
            var uri = $"{_httpClient.BaseAddress}v1/archive?{query}";

            _logger.LogInformation($"Fetching data from {uri}");
            var responseDto = await GetData<MeteoResponseDto>($"{uri}");
            return _mapper.Map<WeatherModel>(responseDto);
        }
        catch (HttpRequestException httpEx)
        {
            _logger.LogError(httpEx, $"Failed to retrieve archived weather data from Meteo Api,\n{httpEx.Message}");
            throw new ExternalApiRequestException();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error archived while fetching from Meteo API");
            throw;
        }
    }
}