using CodeChallenge.Domain.Entities;
using CodeChallenge.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Test.Integration;
public class CodeChallengeWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["GeneralSettings:ShouldRunDataSeeding"] = "false",
                    ["CodeChallengeApiSettings:BaseUrl"] = "https://azfun-impact-code-challenge-api.azurewebsites.net",
                    ["CodeChallengeApiSettings:GetAllProductsEndpoint"] = "/api/GetAllProducts",
                    ["CodeChallengeApiSettings:GetOrderEndpoint"] = "/api/GetOrder/{0}",
                    ["CodeChallengeApiSettings:CreateOrderEndpoint"] = "/api/CreateOrder",
                    ["CodeChallengeApiSettings:LoginEndpoint"] = "/api/Login",
                    ["CodeChallengeApiSettings:DefaultUserEmail"] = "test@test.com",
                    ["CodeChallengeApiSettings:AbsoluteCacheExpirationInMinutes"] = "60"
                });
        });

        builder.ConfigureTestServices(services =>
        {
            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();

            using var context = scope.ServiceProvider.GetRequiredService<CodeChallengeContext>();

            var product = new Product
            {
                Id = 1,
                Name = "Black Damask Shorts, Size 43",
                Price = 93.64M,
                Size = 43,
                Stars = 4
            };

            context.Add(product);

            context.Add(new Basket
            {
                Id = Guid.Parse("029a4482-0fc1-4761-8e6d-7ce31a8dfbdd"),
                CreatedAt = DateTime.UtcNow,
                UserEmail = "test@test.com",
                Items = new List<BasketItem>
                {
                    new()
                    {
                        Id = Guid.Parse("2cdd3a79-9e59-4fc6-ad07-ff7a0451f0c6"),
                        Product = product,
                        Quantity= 1
                    }
                }
            });

            context.SaveChanges();
        });

        base.ConfigureWebHost(builder);
    }
}
