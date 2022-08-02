using MVC.DTOs;

namespace MVC.Models;

public class Order
{
    public Guid? Id { get; set; } = null; // The order id
    public string? CustomerName { get; set; } // The customer name
    public IEnumerable<OrderItem>? Items { get; set; } // The order items

    public decimal Total { get; set; } // The total amount of the order

    public override string ToString() // prints out order ID name and total
    {
        return $"Order #{Id}\n\t{"Placed By:",-15}{CustomerName}\n\t{"Total:",-15}{Total:C2}";
    }
}