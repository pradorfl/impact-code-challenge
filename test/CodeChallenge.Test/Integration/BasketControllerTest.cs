using CodeChallenge.Application.Dtos;
using System.Net;
using System.Net.Http.Json;

namespace CodeChallenge.Test.Integration;
public class BasketControllerTest : IClassFixture<CodeChallengeWebApplicationFactory>
{
    private readonly CodeChallengeWebApplicationFactory _factory;

    public BasketControllerTest(CodeChallengeWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetBasket_ExistingBasketId_ReturnsOk()
    {
        //Arrange
        var client = _factory.CreateClient();
        var basketId = Guid.Parse("029a4482-0fc1-4761-8e6d-7ce31a8dfbdd");

        //Act
        var response = await client.GetAsync($"/Basket/{basketId}");

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        Assert.NotNull(basket);
        Assert.Equal(1, basket!.Items!.Count);
    }

    [Fact]
    public async Task PostBasket_ValidData_ReturnsCreated()
    {
        //Arrange
        var client = _factory.CreateClient();

        var basketDto = new BasketCreationDto
        {
            UserEmail = "test@test.com",
            Items = new List<BasketItemCreationDto>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 3
                }
            }
        };

        //Act
        var response = await client.PostAsJsonAsync("/Basket", basketDto);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        Assert.NotNull(basket);
        Assert.NotEqual(Guid.Empty, basket!.Id);
        Assert.Equal(1, basket!.Items!.Count);
    }

    [Fact]
    public async Task PatchBasket_ValidData_ReturnsOk()
    {
        //Arrange
        var client = _factory.CreateClient();
        var basketId = Guid.Parse("029a4482-0fc1-4761-8e6d-7ce31a8dfbdd");

        var basketItems = new BasketItemCreationDto[]
        {
            new()
            {
                ProductId = 1,
                Quantity = 3
            }
        };

        //Act
        var response = await client.PatchAsync($"/Basket/{basketId}", JsonContent.Create(basketItems));

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        Assert.NotNull(basket);
        Assert.Equal(basketId, basket!.Id);
        Assert.Collection(basket.Items, item =>
        {
            Assert.Equal(1, item.Product!.Id);
            Assert.Equal(3, item.Quantity);
        });
    }

    [Fact]
    public async Task PutBasketItems_ValidData_ReturnsOk()
    {
        //Arrange
        var client = _factory.CreateClient();
        var basketId = Guid.Parse("029a4482-0fc1-4761-8e6d-7ce31a8dfbdd");

        var basketItemUpdate = new BasketItemUpdateDto
        {
            BasketItemId = Guid.Parse("2cdd3a79-9e59-4fc6-ad07-ff7a0451f0c6"),
            Quantity = 5
        };

        //Act
        var response = await client.PutAsJsonAsync($"/Basket/{basketId}/Items", basketItemUpdate);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        Assert.NotNull(basket);
        Assert.Equal(basketId, basket!.Id);
        Assert.Collection(basket.Items, item =>
        {
            Assert.Equal(1, item.Product!.Id);
            Assert.Equal(5, item.Quantity);
        });
    }

    [Fact]
    public async Task DeleteBasketItem_ValidData_ReturnsOk()
    {
        //Arrange
        var client = _factory.CreateClient();
        var basketId = Guid.Parse("029a4482-0fc1-4761-8e6d-7ce31a8dfbdd");
        var basketItemId = Guid.Parse("2cdd3a79-9e59-4fc6-ad07-ff7a0451f0c6");

        //Act
        var response = await client.DeleteAsync($"/Basket/{basketId}/Items/{basketItemId}");

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        Assert.NotNull(basket);
        Assert.Equal(basketId, basket!.Id);
        Assert.Empty(basket.Items);
    }

    [Fact]
    public async Task PostBasketSubmit_ValidData_ReturnsCreated()
    {
        //Arrange
        var client = _factory.CreateClient();

        var basketDto = new BasketCreationDto
        {
            UserEmail = "test@test.com",
            Items = new List<BasketItemCreationDto>
            {
                new()
                {
                    ProductId = 1,
                    Quantity = 3
                }
            }
        };

        var response = await client.PostAsJsonAsync("/Basket", basketDto);
        var basket = await response.Content.ReadFromJsonAsync<BasketDto>();

        //Act
        response = await client.PostAsJsonAsync($"/Basket/{basket!.Id}/Submit", basketDto);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var order = await response.Content.ReadAsStringAsync();
        Assert.NotNull(order);
    }
}
