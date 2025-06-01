using System.Text.Json;
using System.Text;
using Hospital.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Hospital.Application.Services;

public class NationalRegistryService : INationalRegistryService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NationalRegistryService> _logger;
    
    public NationalRegistryService(HttpClient httpClient, IOptions<NationalRegistryConfiguration> configuration, ILogger<NationalRegistryService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (configuration == null || configuration.Value == null)
        {
            _logger.LogError("NationalRegistryConfiguration is null or not provided.");
            throw new ArgumentNullException(nameof(configuration), "NationalRegistryConfiguration cannot be null");
        }

        if (string.IsNullOrEmpty(configuration.Value.Url) || string.IsNullOrEmpty(configuration.Value.ApiKey))
        {
            _logger.LogError("CprValidationApiUrl and NationalRegistryApiKey must be provided in the configuration.");
            throw new ArgumentException("CprValidationApiUrl and NationalRegistryApiKey must be provided in the configuration");
        }

        _httpClient.BaseAddress = new Uri(configuration.Value.Url);
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration.Value.ApiKey}");
    }

    public async Task<bool> ValidateCpr(string cpr)
    {
        var requestBody = new { cpr };
        var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        // var response = await client.PostAsync(CprValidationApiUrl, jsonContent);
        // if (response.IsSuccessStatusCode)
        // {
        //     Console.WriteLine($"[LOG] CPR validation successful for {cpr}");
        //     return true;
        // }
        // else
        // {
        //     Console.WriteLine($"[LOG] CPR validation failed for {cpr}");
        //     return false;
        // }
        await Task.Yield();
        return true; // Dummy response (To be replaced later)

    }
}