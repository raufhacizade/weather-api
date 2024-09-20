using AutoMapper;
using WeatherAPI.DTOs.Responses;
using WeatherAPI.Models;

namespace WeatherAPI.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        // OpenWeather API response DTOs to Models
        CreateMap<OpenWeatherGeoLocationDto, GeoLocationModel>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Lon));
        
        // VisualCrossing API response DTOs to Models
        CreateMap<VisualCrossingDayDto, DetailedWeatherModel>();
        CreateMap<VisualCrossingResponseDto, DetailedWeatherModel>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src=>src.ResolvedAddress))
            .AfterMap((src, dest, context) =>
                context.Mapper.Map(src.Days?[0], dest));

        // OpenMeteo API response DTOs to Models
        CreateMap<MeteoDayDto, WeatherModel>()
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature_2m_mean[0]))
            .ForMember(dest => dest.TemperatureApparent, opt => opt.MapFrom(src => src.Apparent_temperature_mean[0]))
            .ForMember(dest => dest.WindDirection, opt => opt.MapFrom(src => src.Wind_direction_10m_dominant[0]))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind_speed_10m_max[0]))
            .ForMember(dest => dest.RainIntensity, opt => opt.MapFrom(src => src.Rain_sum[0]));
        CreateMap<MeteoResponseDto, WeatherModel>()
            .AfterMap((src, dest, context) =>
                context.Mapper.Map(src.Daily, dest));

        // Tomorrow API response DTOs to Models
        CreateMap<TomorrowLocationDto, WeatherModel>()
            .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Lat))
            .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Lon));
        CreateMap<TomorrowValuesDto, WeatherModel>();
        CreateMap<TomorrowDataDto, WeatherModel>();
        CreateMap<TomorrowResponseDto, WeatherModel>()
            .AfterMap((src, dest, context) =>
                context.Mapper.Map(src.Location, dest))
            .AfterMap((src, dest, context) =>
                context.Mapper.Map(src.Data, dest))
            .AfterMap((src, dest, context) =>
                context.Mapper.Map(src.Data?.Values, dest));
        
        // from Models to our WeatherAPI DTOs
        CreateMap<GeoLocationModel, GeoLocationDto>();
        CreateMap<DetailedWeatherModel, DetailedWeatherDto>();
        CreateMap<WeatherModel, WeatherDto>();
        
    }
}