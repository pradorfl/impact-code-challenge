using CodeChallenge.Domain.Settings;
using CodeChallenge.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CodeChallenge.Infrastructure.HttpHandlers;
public sealed class CodeChallengeLoginHandler : DelegatingHandler
{
    private const string TokenCacheKey = "TokenCache";

    private readonly CodeChallengeApiSettings _settings;
    private readonly ILogger<CodeChallengeLoginHandler> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    public CodeChallengeLoginHandler(
        IOptions<CodeChallengeApiSettings> options,
        ILogger<CodeChallengeLoginHandler> logger,
        IHttpClientFactory httpClientFactory,
        IMemoryCache memoryCache)
    {
        _settings = options.Value;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _memoryCache.GetOrCreateAsync(TokenCacheKey, GenerateTokenAsync);

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<string?> GenerateTokenAsync(ICacheEntry entry)
    {
        try
        {
            _logger.LogTrace($"{nameof(CodeChallengeLoginHandler)} - calling code challenge login");

            using var httpClient = _httpClientFactory.CreateClient("CodeChallengeLogin");

            var jsonContent = JsonContent.Create(new
            {
                email = _settings.DefaultUserEmail
            });

            var response = await httpClient.PostAsync(_settings.LoginEndpoint, jsonContent);

            response.EnsureSuccessStatusCode();

            var tokenObject = await response.Content.ReadFromJsonAsync<CodeChallengeToken>();

            if (!string.IsNullOrWhiteSpace(tokenObject?.Token))
            {
                entry.SetValue(tokenObject.Token);
                entry.SetAbsoluteExpiration(DateTime.UtcNow.AddMinutes(_settings.AbsoluteCacheExpirationInMinutes));

                _logger.LogTrace($"{nameof(CodeChallengeLoginHandler)} - code challenge login succesful");
            }
            else
            {
                _logger.LogTrace($"{nameof(CodeChallengeLoginHandler)} - code challenge login unsuccesful");
            }

            return tokenObject?.Token;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(CodeChallengeLoginHandler)} - error calling code challenge login");
            throw;
        }
    }
}
