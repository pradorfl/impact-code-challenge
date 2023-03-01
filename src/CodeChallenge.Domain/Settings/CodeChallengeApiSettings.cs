namespace CodeChallenge.Domain.Settings;
public record CodeChallengeApiSettings
{
#nullable disable
    public string BaseUrl { get; set; }
    public string GetAllProductsEndpoint { get; set; }
    public string GetOrderEndpoint { get; set; }
    public string CreateOrderEndpoint { get; set; }
    public string LoginEndpoint { get; set; }
    public string DefaultUserEmail { get; set; }
    public int AbsoluteCacheExpirationInMinutes { get; set; }
#nullable enable
}
