using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.DTOs.Requests;

public class CityCountryDto
{
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
}