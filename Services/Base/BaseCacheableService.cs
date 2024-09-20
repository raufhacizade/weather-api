using Microsoft.Extensions.Caching.Memory;

namespace WeatherAPI.Services.Base;

public abstract class BaseCacheableService(IMemoryCache cache, ILogger logger) : BaseService(logger)
{
}