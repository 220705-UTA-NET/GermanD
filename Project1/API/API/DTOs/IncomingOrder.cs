using API.Models;

namespace API.DTOs;

public class IncomingOrder
{
    public string? CustomerName { get; set; } //get customer name 
    public IEnumerable<OrderItem>? Items { get; set; } //get order item list
}