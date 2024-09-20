using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs.Responses;

public class MeteoResponseDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public MeteoDayDto Daily { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public class MeteoDayDto
{
    public List<double?> Temperature_2m_mean { get; set; }
    public List<double?> Apparent_temperature_mean { get; set; }
    public List<double?> Wind_direction_10m_dominant { get; set; }
    public List<double?> Wind_speed_10m_max { get; set; }
    public List<double?> Rain_sum { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}