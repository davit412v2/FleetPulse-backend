using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Simulator.Models;

namespace Simulator.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly ITokenStore _tokenStore;

    public AuthenticationService(HttpClient httpClient, ILogger<AuthenticationService> logger, ITokenStore tokenStore)
    {
        _httpClient = httpClient;
        _logger = logger;
        _tokenStore = tokenStore;
    }

    public async Task LoginAsync(string email, string password)
    {
        try
        {
            var loginRequest = new LoginRequest { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("/api/Authentication/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var loginResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>();
            _logger.LogInformation("✅ Login exitoso para {Email}", email);

            _tokenStore.Token = loginResponse!.Data.Token;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Error al hacer login");
            throw;
        }
    }
}
