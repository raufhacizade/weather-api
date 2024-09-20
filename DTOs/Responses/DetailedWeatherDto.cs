namespace WeatherAPI.DTOs.Responses;

public class DetailedWeatherDto
{
    public string? Location { get; set; }
    public string? Conditions { get; set; }
    public string? Description { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Timezone { get; set; }
    public string? Datetime { get; set; }
    public double? TempMax { get; set; }
    public double? TempMin { get; set; }
    public double? Temp { get; set; }
    public double? FeelslikeMax { get; set; }
    public double? FeelslikeMin { get; set; }
    public double? Feelslike { get; set; }
    public double? Dew { get; set; }
    public double? Humidity { get; set; }
    public double? Snow { get; set; }
    public double? Snowdepth { get; set; }
    public double? Windgust { get; set; }
    public double? Windspeed { get; set; }
    public double? Winddir { get; set; }
    public double? Pressure { get; set; }
    public double? Cloudcover { get; set; }
    public double? Visibility { get; set; }
    public double? Solarradiation { get; set; }
    public double? Solarenergy { get; set; }
    public string? Sunrise { get; set; }
    public string? Sunset { get; set; }
    public double? Moonphase { get; set; }
}