namespace MVC.Models;

public class Item
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ItemType Type { get; set; }


    public override string ToString()
    {
        return $"{Name,-10} {Price,-5:C2}";
        // Here C2 means:
        //  C -> Currency
        //  2 -> 2 digits after the decimal point
    }
}