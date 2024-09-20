using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs.Responses;

public class OpenWeatherGeoLocationDto
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}