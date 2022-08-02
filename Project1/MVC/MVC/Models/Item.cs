namespace MVC.Models;

public class Item
{
    public Guid Id { get; set; } // Unique identifier
    public string? Name { get; set; } //name of menu item
    public decimal Price { get; set; } // price of menu item
    public ItemType Type { get; set; } // type of menu item such as food or drink


    public override string ToString() //Prints out the name and price of the menu item
    {
        return $"{Name,-30} {Price,-5:C2}";
        // Here C2 means:
        //  C -> Currency
        //  2 -> 2 digits after the decimal point
    }
}