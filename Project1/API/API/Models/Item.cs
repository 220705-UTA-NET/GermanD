namespace API.Models;

public class Item
{
    public Guid Id { get; set; } // item id
    public string? Name { get; set; } // item name
    public decimal Price { get; set; } // item price
    public ItemType Type { get; set; } // item type
}