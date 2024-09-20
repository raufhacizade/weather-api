using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.DTOs.Requests;

public class CityCountryDateDto
{
    [Required]
    public string City { get; set; }
    [Required]
    public string Country { get; set; }
    [Required]
    public string Date { get; set; }
 
}