using CodeChallenge.Domain.Clients;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CodeChallenge.Infrastructure.Clients;
public sealed class CodeChallengeApiClient : ICodeChallengeApiClient
{
    private readonly CodeChallengeApiSettings _settings;
    private readonly ILogger<CodeChallengeApiClient> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public CodeChallengeApiClient(
        IOptions<CodeChallengeApiSettings> options,
        ILogger<CodeChallengeApiClient> logger,
        IHttpClientFactory httpClientFactory)
    {
        _settings = options.Value;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Order> GetOrderAsync(string orderId)
    {
        using var httpClient = _httpClientFactory.CreateClient("CodeChallenge");

        var response = await httpClient.GetAsync(string.Format(_settings.GetOrderEndpoint, orderId));

        response.EnsureSuccessStatusCode();

        var order = await response.Content.ReadFromJsonAsync<Order>();

        return order!;
    }

    public async Task<Order> CreateOrderAsync(Order newOrder)
    {
        using var httpClient = _httpClientFactory.CreateClient("CodeChallenge");

        var response = await httpClient.PostAsJsonAsync(_settings.CreateOrderEndpoint, newOrder);

        response.EnsureSuccessStatusCode();

        var order = await response.Content.ReadFromJsonAsync<Order>();

        return order!;
    }
}
