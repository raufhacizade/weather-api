using System.Text.Json;
using System.Text.Json.Serialization;

namespace WeatherAPI.Models;

public class GeoLocationModel
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public GeoLocationModel()
    {
        
    }
    
    public GeoLocationModel((double, double) locations)
    {
        Latitude = locations.Item1;
        Longitude = locations.Item2;
    }
}