using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;

namespace WeatherAPI.Clients.Base;

public abstract class BaseApiClient(HttpClient httpClient, IMapper mapper, ILogger logger)
{
    protected readonly HttpClient _httpClient = httpClient;
    protected readonly IMapper _mapper = mapper;
    protected readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
    };
    protected readonly ILogger _logger = logger;

    protected async Task<TResponse> GetData<TResponse>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Request was successful!");
            var data = await response.Content.ReadFromJsonAsync<TResponse>(_serializerOptions);
            return data;
        }
        
        throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
    }
}