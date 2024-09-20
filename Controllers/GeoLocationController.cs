using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherAPI.DTOs.Requests;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Services.Interfaces;

namespace WeatherAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeoLocationController(IMapper mapper, IGeoLocationService geoLocationService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GeoLocationDto>> GetGeoLocation([FromQuery] CityCountryDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var lonAndLongitude = await geoLocationService.GetLatitudeAndLongitudeAsync(request.City, request.Country);
        return mapper.Map<GeoLocationDto>(lonAndLongitude);
    }
}