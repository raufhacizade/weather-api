using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.DTOs.Responses;

public class TomorrowResponseDto
{
    public TomorrowDataDto? Data { get; set; }
    public TomorrowLocationDto? Location { get; set; }
}

public class TomorrowDataDto
{
    public TomorrowValuesDto? Values { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public class TomorrowValuesDto
{
    public double? Temperature { get; set; }
    public double? TemperatureApparent { get; set; }
    public double? WindDirection { get; set; }
    public double? WindSpeed { get; set; }
    public double? RainIntensity { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}

public class TomorrowLocationDto
{
    public double? Lat { get; set; }
    public double? Lon { get; set; }
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? ExtensionData { get; set; }
}
