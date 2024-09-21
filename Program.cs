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

var keyVaultUrl = configuration.GetSection("keyVault:keyVaultUrl");
var keyVaultClientId = configuration.GetSection("keyVault:ClientId");
var keyVaultClientSecret = configuration.GetSection("keyVault:ClientSecret");
var keyVaultDirectoryId = configuration.GetSection("keyVault:DirectoryId");

var credential = new ClientSecretCredential(keyVaultDirectoryId.Value!.ToString(), keyVaultClientId.Value!.ToString(),
    keyVaultClientSecret.Value!.ToString());

configuration.AddAzureKeyVault(keyVaultUrl.Value!.ToString(), keyVaultClientId.Value!.ToString(),
    keyVaultClientSecret.Value!.ToString(), new DefaultKeyVaultSecretManager());

var secretClient = new SecretClient(new Uri(keyVaultUrl.Value!.ToString()), credential);

builder.Services.AddMemoryCache();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.ConfigureDependencies(configuration, secretClient);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
