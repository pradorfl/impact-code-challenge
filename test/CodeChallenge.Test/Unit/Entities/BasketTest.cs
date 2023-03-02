using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Test.Unit.Entities;
public class BasketTest
{
    [Fact]
    public void Constructor_SetValuesAccordingly()
    {
        //Arrange
        var userEmail = "test@test.com";
        var basketItems = new List<BasketItem>
        {
            new()
            {
                Quantity = 1,
                Product = new()
                {
                    Id = 1,
                    Name = "Test product"
                }
            }
        };

        //Act
        var basket = new Basket(userEmail, basketItems);

        //Assert
        Assert.NotEqual(Guid.Empty, basket.Id);
        Assert.NotEqual(DateTime.MinValue, basket.CreatedAt);
        Assert.Equal(userEmail, basket.UserEmail);
        Assert.Equal(1, basket.Items.Count);
        Assert.Collection(basket.Items, item =>
        {
            Assert.Equal(1, item.Quantity);
            Assert.Equal(1, item.Product!.Id);
            Assert.Equal("Test product", item.Product!.Name);
        });
    }

    [Fact]
    public void RemoveItem_NonExistingItem_ThrowsException()
    {
        //Arrange
        var basket = new Basket();

        //Act
        void Act() => basket.RemoveItem(Guid.Empty);

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Fact]
    public void RemoveItem_ExistingItem_ItemIsRemoved()
    {
        //Arrange
        var basketItemId = Guid.NewGuid();

        var basket = new Basket
        {
            Items = new List<BasketItem>
            {
                new() { Id = basketItemId }
            }
        };

        //Act
        basket.RemoveItem(basketItemId);

        //Assert
        Assert.Empty(basket.Items);
    }

    [Fact]
    public void UpdateItemQuantity_NonExistingItem_ThrowsException()
    {
        //Arrange
        var basket = new Basket();

        //Act
        void Act() => basket.UpdateItemQuantity(Guid.Empty, 0);

        //Assert
        Assert.Throws<ArgumentOutOfRangeException>(Act);
    }

    [Fact]
    public void UpdateItemQuantity_ExistingItem_ItemQuantityIsUpdated()
    {
        //Arrange
        var basketItemId = Guid.NewGuid();

        var basket = new Basket
        {
            Items = new List<BasketItem>
            {
                new()
                {
                    Id = basketItemId,
                    Quantity = 1
                }
            }
        };

        //Act
        basket.UpdateItemQuantity(basketItemId, 2);

        //Assert
        Assert.Collection(basket.Items, item =>
        {
            Assert.Equal(2, item.Quantity);
        });
    }

    [Fact]
    public void UpdateItemQuantity_QuantityIsZero_ItemIsRemoved()
    {
        //Arrange
        var basketItemId = Guid.NewGuid();

        var basket = new Basket
        {
            Items = new List<BasketItem>
            {
                new()
                {
                    Id = basketItemId,
                    Quantity = 1
                }
            }
        };

        //Act
        basket.UpdateItemQuantity(basketItemId, 0);

        //Assert
        Assert.Empty(basket.Items);
    }

    [Fact]
    public void MergeItems_ExistingItem_ItemQuantityIsUpdated()
    {
        //Arrange
        var productId = 1;

        var basket = new Basket
        {
            Items = new List<BasketItem>
            {
                new()
                {
                    Product = new() { Id = productId },
                    Quantity = 1
                }
            }
        };

        var newItems = new List<BasketItem>
        {
            new()
            {
                Product = new() { Id = productId },
                Quantity = 2
            }
        };

        //Act
        var nonExistingItems = basket.MergeItems(newItems);

        //Assert
        Assert.Empty(nonExistingItems);

        Assert.Collection(basket.Items, item =>
        {
            Assert.Equal(2, item.Quantity);
        });
    }

    [Fact]
    public void MergeItems_NonExistingItem_ReturnsNonExistingItems()
    {
        //Arrange
        var basket = new Basket();

        var newItems = new List<BasketItem>
        {
            new()
            {
                Product = new() { Id = 1 },
                Quantity = 1
            }
        };

        //Act
        var nonExistingItems = basket.MergeItems(newItems);

        //Assert
        Assert.Equal(1, nonExistingItems.Count);

        Assert.Collection(nonExistingItems, item =>
        {
            Assert.Equal(basket, item.Basket);
            Assert.Equal(1, item.Product!.Id);
            Assert.Equal(1, item.Quantity);
        });
    }

    [Fact]
    public void MergeItems_EmptyNewItemsCollection_ReturnsEmpty()
    {
        //Arrange
        var basket = new Basket();
        var newItems = new List<BasketItem>(0);

        //Act
        var nonExistingItems = basket.MergeItems(newItems);

        //Assert
        Assert.Equal(0, nonExistingItems.Count);
    }
}
