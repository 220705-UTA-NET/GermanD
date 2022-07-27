using System.Text;

namespace MVC.Models;

public class Menu
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<Item> Items { get; set; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder($"-----{Name}-----\n");

        foreach (var item in Items)
        {
            stringBuilder
                .Append(item)
                .Append('\n');
        }

        stringBuilder.Remove(stringBuilder.Length - 1, 1);

        return stringBuilder.ToString();
    }
}