using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Domain.Clients;
public interface ICodeChallengeApiClient
{
    Task<Order> GetOrderAsync(string orderId);
    Task<Order> CreateOrderAsync(Order newOrder);
}
