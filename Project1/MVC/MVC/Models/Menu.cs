using System.Text;

namespace MVC.Models;

public class Menu
{
    public Guid Id { get; set; } // id of menu item
    public string? Name { get; set; } // name of menu item
    public IEnumerable<Item> Items { get; set; } // list of items in menu

    public override string ToString()
    {
        var stringBuilder = new StringBuilder($"-----{Name}-----\n"); // prints out name of menu and items in said menu

        foreach (var item in Items) // prints out name and price for each item in menu
        {
            stringBuilder
                .Append(item)
                .Append('\n');
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);

        return stringBuilder.ToString();
    }
}