namespace CodeChallenge.Domain.Entities;
public sealed class Basket
{
    public Basket(string userEmail, IList<BasketItem> basketItems)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UserEmail = userEmail;
        Items = basketItems;
    }

    public Basket()
    {
        Items = new HashSet<BasketItem>();
    }

    public Guid Id { get; set; }
    public string? UserEmail { get; set; }
    public ICollection<BasketItem> Items { get; set; }
    public DateTime CreatedAt { get; set; }

    public IList<BasketItem> MergeItems(IList<BasketItem> newItems)
    {
        if (newItems is null || newItems.Count == 0)
            return new List<BasketItem>(0);

        var nonExistingItems = new List<BasketItem>(newItems.Count);

        foreach (var newItem in newItems)
        {
            var existingItem = Items.FirstOrDefault(x => x.Product!.Id == newItem.Product!.Id);

            if (existingItem is null)
            {
                newItem.Basket = this;
                nonExistingItems.Add(newItem);
            }
            else
            {
                existingItem.Quantity = newItem.Quantity;

                if (existingItem.Quantity <= 0) Items.Remove(existingItem);
            }
        }

        return nonExistingItems;
    }

    public void UpdateItemQuantity(Guid basketItemId, int quantity)
    {
        var item = Items.FirstOrDefault(x => x.Id == basketItemId);

        if (item is null)
            throw new ArgumentOutOfRangeException("basketItemId", $"BasketItem id '{basketItemId}' was not found.");

        item.Quantity = quantity;

        if (item.Quantity <= 0) Items.Remove(item);
    }

    public void RemoveItem(Guid basketItemId)
    {
        var item = Items.FirstOrDefault(x => x.Id == basketItemId);

        if (item is null)
            throw new ArgumentOutOfRangeException("basketItemId", $"BasketItem id '{basketItemId}' was not found.");

        Items.Remove(item);
    }
}
