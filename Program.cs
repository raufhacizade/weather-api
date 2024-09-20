using WeatherAPI.Configuration;
using WeatherAPI.Mapping;
using WeatherAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureDependencies();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
