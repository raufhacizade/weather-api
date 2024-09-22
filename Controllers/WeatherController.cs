using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DTOs.Requests;
using WeatherAPI.Exceptions;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Services.Interfaces;
using WeatherAPI.Validators;

namespace WeatherAPI.Controllers;

[Route("api/[controller]")]
public class WeatherController(IMapper mapper, IWeatherService weatherService, IGeoLocationService geoLocationService)
    : ControllerBase
{
    private readonly DateValidator _visualCrossingDateValidator = new(new DateOnly(1950, 1, 1), new DateOnly(2050, 12, 31));
    private readonly DateValidator _meteoDateValidator = new(new DateOnly(1940, 1, 1), DateOnly.FromDateTime(DateTime.Now));

    [HttpGet("archived")]
    public async Task<ActionResult<WeatherDto>> GetArchivedWeather([FromQuery] CityCountryDateDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var dateValidationResult = _meteoDateValidator.Validate(request.Date);
        if (!dateValidationResult.IsValid)
            throw new InvalidDataException(dateValidationResult.ErrorMessage);

        var location = await geoLocationService.GetLatitudeAndLongitudeAsync(request.City, request.Country);
        if (location == null)
            throw new InValidCityOrCountryName();

        var weatherData =
            await weatherService.GetArchivedWeatherInfoByDayAsync(location.Longitude, location.Latitude,
                dateValidationResult.Date);
        return mapper.Map<WeatherDto>(weatherData);
    }


    [HttpGet("detailed")]
    public async Task<ActionResult<DetailedWeatherDto>> GetDetailedWeather([FromQuery] CityCountryDateDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var dateValidationResult = _visualCrossingDateValidator.Validate(request.Date);
        if (!dateValidationResult.IsValid)
            throw new InvalidDataException(dateValidationResult.ErrorMessage);

        if (await geoLocationService.GetLatitudeAndLongitudeAsync(request.City, request.Country) == null)
            throw new InValidCityOrCountryName();

        var result = await weatherService.GetDetailedWeatherInfoByDayAsync(request.City, request.Country, dateValidationResult.Date);
        return mapper.Map<DetailedWeatherDto>(result);
    }
    
    [HttpGet("today")]
    public async Task<ActionResult<WeatherDto>> GetTodayWeatherByCity([FromQuery] CityCountryDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (await geoLocationService.GetLatitudeAndLongitudeAsync(request.City, request.Country) == null)
            throw new InValidCityOrCountryName();
        
        var location = await geoLocationService.GetLatitudeAndLongitudeAsync(request.City, request.Country);
        if (location == null)
            throw new InValidCityOrCountryName();

        var weatherData = await weatherService.GetTodayWeatherInfoAsync(location.Latitude, location.Longitude);
        return mapper.Map<WeatherDto>(weatherData);
    }
}