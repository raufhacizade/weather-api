using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using WeatherAPI.Configuration;
using WeatherAPI.Mapping;
using WeatherAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var keyVaultUrl = configuration.GetSection("KeyVault:KeyVaultUrl");
var keyVaultClientId = configuration.GetSection("KeyVault:ClientId");
var keyVaultClientSecret = configuration.GetSection("KeyVault:ClientSecret");
var keyVaultDirectoryId = configuration.GetSection("KeyVault:DirectoryId");

var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!, keyVaultClientId.Value!,
    keyVaultClientSecret.Value!);

configuration.AddAzureKeyVault(keyVaultUrl.Value!, keyVaultClientId.Value!,
    keyVaultClientSecret.Value!, new DefaultKeyVaultSecretManager());

var secretClient = new SecretClient(new Uri(keyVaultUrl.Value!), credential);

builder.Services.AddMemoryCache();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureDependencies(configuration, secretClient);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();