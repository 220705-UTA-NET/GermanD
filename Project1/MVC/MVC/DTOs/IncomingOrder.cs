using MVC.Models;

namespace MVC.DTOs;

public class IncomingOrder
{
    public string? CustomerName { get; set; } 
    public IEnumerable<OrderItem>? Items { get; set; } // order items list
}