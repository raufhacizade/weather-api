namespace WeatherAPI.DTOs.Responses;

public class WeatherDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? Temperature { get; set; }
    public double? TemperatureApparent { get; set; }
    public double? WindDirection { get; set; }
    public double? WindSpeed { get; set; }
    public double? RainIntensity { get; set; }
}