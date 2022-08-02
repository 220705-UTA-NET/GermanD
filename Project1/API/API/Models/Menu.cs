namespace API.Models;

public class Menu
{
    public Guid Id { get; set; } // menu id
    public string? Name { get; set; } // menu name
    public IList<Item> Items { get; set; } = new List<Item>(); // menu item list
}