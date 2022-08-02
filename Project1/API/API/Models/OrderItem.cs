namespace API.Models;

public class OrderItem
{
    public Item? Item { get; set; } // item ordered by customer
    public int Quantity { get; set; } // quantity of item ordered
}