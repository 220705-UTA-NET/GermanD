using MVC.DTOs;

namespace MVC.Models;

public class Order
{
    public Guid? Id { get; set; } = null;
    public string? CustomerName { get; set; }
    public IEnumerable<OrderItem> Items { get; set; }

    public decimal Total { get; set; }

    public override string ToString()
    {
        return $"Order #{Id}\n\t{"Placed By:",-15}{CustomerName}\n\t{"Total:",-15}{Total:C2}";
    }
}